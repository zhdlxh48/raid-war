using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Earthquake : AttackActionBase
    {
        public Projector projection;
        public EarthquakeEffect effect;
        public Transform Target;
        public Transform mTran;
        public AudioClip soundEffect;

        public float activeTime = 2;
        private float ratioPerSec = 0.0f;
        private IEnumerator coru;

        private bool soundCheck = true;
        // Start is called before the first frame update
        void Awake()
        {
            mTran = transform.root;

            projection.gameObject.SetActive(false);

            projection.aspectRatio = 0.01f;
            projection.orthographicSize = attackRange;
            ratioPerSec = (1.0f / activeTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (projection.gameObject.activeSelf)
            {
                Vector3 dir = Target.position - mTran.position;
                dir.y = 0.0f;

                projection.aspectRatio += ratioPerSec * Time.deltaTime;

                if (projection.aspectRatio >= 0.75f)
                {
                    Core.SoundManager.OneShot(soundEffect);
                    mTran.GetComponent<GolemBehavior>().camShake.Invoke();
                    coru = effect.efstart(sumDamage);
                    StartCoroutine(coru);
                    projection.aspectRatio = 0.1f;
                    projection.gameObject.SetActive(false);
                }
                else if (projection.aspectRatio <= 0.3f)
                {
                    effect.transform.rotation = Quaternion.LookRotation(dir);
                    mTran.rotation = Quaternion.LookRotation(dir);
                }
            }
        }

        int sumDamage = 0;

        public override void ExcuteSkill(int damage)
        {
           
            Vector3 temp = mTran.position;
            temp.y = 0.0f;
            effect.transform.position = temp;

            sumDamage = damage * skillFactor;
            projection.gameObject.SetActive(true);
        }

        public override void StopSkill()
        {
            StopCoroutine(coru);
        }
    }
}