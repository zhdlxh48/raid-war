using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RushAttack : AttackActionBase
    {
        public Projector projection;
        public GolemBehavior golem;

        public float activeTime = 2;
        public float RushSpeed = 10.0f;

        private float ratioPerSec = 0.0f;
        private float rockStartY = 0.0f;
        private Collider collider;
        private IEnumerator coru;

        Vector3 temp;

        void Awake()
        {
            golem = transform.root.GetComponent<GolemBehavior>();
            collider = GetComponent<Collider>();
            collider.enabled = false;
            projection.gameObject.SetActive(false);

            projection.aspectRatio = 0.01f;
            projection.orthographicSize = attackRange;
            ratioPerSec = (1.0f / activeTime);
        }

        void Update()
        {
            transform.position = golem.trans.position;
            if (projection.gameObject.activeSelf)
            {
                projection.aspectRatio += ratioPerSec * Time.deltaTime;

                if (projection.aspectRatio >= 0.75f)
                {
                    projection.aspectRatio = 0.1f;
                    projection.gameObject.SetActive(false);
                    coru = Rush();
                    StartCoroutine(coru);
                }
            }
        }


        public override void ExcuteSkill(int damage)
        {
            temp = golem.closePlayerTrans.position;

            Vector3 dir = golem.closePlayerTrans.position - golem.trans.position;
            dir.y = 0.0f;
            golem.trans.rotation = Quaternion.LookRotation(dir);
            sumDamage = damage * skillFactor;
            projection.gameObject.SetActive(true);
        }

        public override void StopSkill()
        {
            StopCoroutine(coru);
        }

        private int sumDamage = 0;

        IEnumerator Rush()
        {
            Vector3 dir = golem.trans.position - temp;
            collider.enabled = true;
            while (dir.sqrMagnitude > 0.2f * 0.2f)
            {
                golem.trans.position = Vector3.MoveTowards(golem.trans.position, temp, RushSpeed * Time.deltaTime);
                dir = golem.trans.position - temp;
                yield return null;
            }
            collider.enabled = false;
            golem.attackFlag = true;
            yield return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (golem.attackFlag)
                {
                    golem.playerOnDamageRig.Invoke(sumDamage, other.transform.position - golem.trans.position);
                    golem.attackFlag = false;
                }
            }
        }
    }
}