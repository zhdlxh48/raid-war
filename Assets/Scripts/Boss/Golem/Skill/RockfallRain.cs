using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockfallRain : AttackActionBase
    {
        public GameObject rockOb;
        public int DropCount = 12;
        public float fallInterval = 0.05f;
        public float fallSpeed = 50.0f; // 투사체 속도
        public float activeTime = 2;

        private GolemBehavior golem;

        private WaitForSeconds wait;
        private List<GameObject> rockPool;
        private IEnumerator coru;

        // Start is called before the first frame update
        void Awake()
        {
            golem = transform.root.GetComponent<GolemBehavior>();
            rockPool = new List<GameObject>();
            wait = new WaitForSeconds(fallInterval);

            for (int i = 0; i < DropCount; i++)
            {
                var rock = Instantiate(rockOb);
                rock.hideFlags = HideFlags.HideInHierarchy;
                rock.SetActive(false);
               // rock.Initialize(attackRange, fallSpeed, activeTime, minDamage, maxDamage);
                rockPool.Add(rock);
            }
        }

        public override void ExcuteSkill(int damage)
        {
            golem.camShake.Invoke();
            coru = Skill(damage);
            StartCoroutine(coru);
        }

        public override void StopSkill()
        {
            StopCoroutine(coru);
        }

        IEnumerator Skill(int damage)
        {
            RendomPoint(DropCount);
            for (int i = 0; i < DropCount; i++)
            {
                rockPool[i].SetActive(true);
                rockPool[i].transform.position = tempV[i];
                rockPool[i].GetComponentInChildren<RockRockScript>().SetRockInfo(golem, damage * skillFactor);

                yield return wait;
            }
            tempV.Clear();
        }

        private List<Vector3> tempV = new List<Vector3>();
        public void RendomPoint(int DropCount)
        {
            Vector3 retVal;
            bool cheak;
            for (int i = 0; i < DropCount; i++)
            {
                do
                {
                    cheak = false;
                    Vector2 ranV = Random.insideUnitCircle * 13.3f;

                    retVal.x = ranV.x;
                    retVal.y = 30.0f;
                    retVal.z = ranV.y;
                    for (int c = 0; c < tempV.Count; c++)
                    {
                        if ((tempV[c] - retVal).sqrMagnitude <= 2.3f * 2.3f)
                        {
                            cheak |= true;
                        }
                    }

                } while (cheak);

                tempV.Add(retVal);
            }
        }
    }
}