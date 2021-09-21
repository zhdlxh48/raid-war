using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockThrow : AttackActionBase
    {
        List<RockThrowObject> rockPool;

        public float interval = 0.5f;
        public float projectSpeed = 20.0f;
        public GameObject rockOb;
        public AudioClip soundEffect;

        private GolemBehavior golem;
        private Transform tr;
        private Vector3[] anglePoints = new Vector3[12];
        private IEnumerator coru;

        private WaitForSeconds throwInterval;
        // Start is called before the first frame update
        void Awake()
        {
            golem = transform.root.GetComponent<GolemBehavior>();
            rockPool = new List<RockThrowObject>();
            throwInterval = new WaitForSeconds(interval);
            tr = GetComponent<Transform>();

            float angleTemp = Mathf.PI / 3;

            anglePoints[0] = new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0));
            anglePoints[1] = new Vector3(Mathf.Cos(angleTemp), 0, Mathf.Sin(angleTemp));
            anglePoints[2] = new Vector3(Mathf.Cos(angleTemp * 2), 0, Mathf.Sin(angleTemp * 2));
            anglePoints[3] = new Vector3(Mathf.Cos(angleTemp * 3), 0, Mathf.Sin(angleTemp * 3));
            anglePoints[4] = new Vector3(Mathf.Cos(angleTemp * 4), 0, Mathf.Sin(angleTemp * 4));
            anglePoints[5] = new Vector3(Mathf.Cos(angleTemp * 5), 0, Mathf.Sin(angleTemp * 5));

            float temp = angleTemp / 2;

            anglePoints[6] = new Vector3(Mathf.Cos(temp), 0, Mathf.Sin(temp));
            anglePoints[7] = new Vector3(Mathf.Cos(temp * 3), 0, Mathf.Sin(temp * 3));
            anglePoints[8] = new Vector3(Mathf.Cos(temp * 5), 0, Mathf.Sin(temp * 5));
            anglePoints[9] = new Vector3(Mathf.Cos(temp * 7), 0, Mathf.Sin(temp * 7));
            anglePoints[10] = new Vector3(Mathf.Cos(temp * 9), 0, Mathf.Sin(temp * 9));
            anglePoints[11] = new Vector3(Mathf.Cos(temp * 11), 0, Mathf.Sin(temp * 11));

            for (int i = 0; i < 12; i++)
            {
                var rock = Instantiate(rockOb, tr.position + anglePoints[i] * 2.0f, Quaternion.identity).GetComponent<RockThrowObject>();
                rock.gameObject.hideFlags = HideFlags.HideInHierarchy;
                rock.gameObject.SetActive(false);
                rockPool.Add(rock);
            }
        }

        public void Update()
        {
            int i = 0;
            for (; i < 12; i++)
            {
                if (rockPool[i].gameObject.activeSelf)
                {
                    rockPool[i].transform.Translate(anglePoints[i] * projectSpeed * Time.deltaTime);
                    if ((rockPool[i].transform.position - tr.position).sqrMagnitude > 18.0f * 18.0f)
                    {
                        rockPool[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        public override void ExcuteSkill(int damage)
        {
            coru = Skill(damage * skillFactor);
            StartCoroutine(coru);
        }

        public override void StopSkill()
        {
            StopCoroutine(coru);
        }

        WaitForSeconds wait = new WaitForSeconds(1.2f);

        IEnumerator Skill(int damage)
        {
            yield return wait;
            int i = 0;
            Vector3 temp = new Vector3(0.0f, 1.0f, 0.0f);
            golem.camShake.Invoke();
            Core.SoundManager.OneShot(soundEffect);
            for (; i < 6; i++)
            {
                rockPool[i].transform.position = tr.position + (anglePoints[i] * 0.5f) + temp;
                rockPool[i].SetRockInfo(golem, damage);
                rockPool[i].gameObject.SetActive(true);
            }

            yield return throwInterval;
            golem.camShake.Invoke();
            Core.SoundManager.OneShot(soundEffect);
            for (; i < 12; i++)
            {
                rockPool[i].transform.position = tr.position + (anglePoints[i] * 0.5f) + temp;
                rockPool[i].SetRockInfo(golem, damage);
                rockPool[i].gameObject.SetActive(true);
            }
        }
    }
}