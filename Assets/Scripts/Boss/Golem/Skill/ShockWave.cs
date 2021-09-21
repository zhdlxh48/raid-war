using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class ShockWave : AttackActionBase
    {
        public Projector attackChargingPro;
        public Projector attackRangePro;
        public GameObject effect;
        public AudioClip soundEffect;
        public float activeTime = 2;

        public GolemBehavior golem;

        private float orSizePerSec = 0.0f;
        private float rockStartY = 0.0f;
        private int sumDamage = 0;
        // Start is called before the first frame update
        void Awake()
        {
            golem = transform.root.GetComponent<GolemBehavior>();
            attackChargingPro.gameObject.SetActive(false);
            attackRangePro.gameObject.SetActive(false);
            effect.SetActive(false);
            effect.hideFlags = HideFlags.HideInHierarchy;
            attackChargingPro.orthographicSize = 0.0f;
            attackRangePro.orthographicSize = attackRange;
            orSizePerSec = (attackRange / activeTime);
        }

        // Update is called once per frame
        void Update()
        {
            if (attackChargingPro.gameObject.activeSelf && attackRangePro.gameObject.activeSelf && can)
            {
                attackChargingPro.orthographicSize += orSizePerSec * Time.deltaTime;

                if (attackChargingPro.orthographicSize >= attackRange)
                {
                    golem.camShake.Invoke();
                    Core.SoundManager.OneShot(soundEffect);
                    Vector3 temp = this.transform.position;
                    temp.y = effect.transform.position.y;
                    effect.transform.position = temp;
                    effect.SetActive(true);
                    if ((temp - golem.closePlayerTrans.position).sqrMagnitude < 9.5f * 9.5f)
                        golem.playerOnDamageRig.Invoke(sumDamage, Vector3.zero);

                    attackChargingPro.orthographicSize = 0.0f;
                    attackChargingPro.gameObject.SetActive(false);
                    attackRangePro.gameObject.SetActive(false);
                }
            }
        }

        public override void ExcuteSkill(int damage)
        {
            can = true;
            sumDamage = damage * skillFactor;
            effect.SetActive(false);
            attackChargingPro.gameObject.SetActive(true);
            attackRangePro.gameObject.SetActive(true);
            Debug.Log("ShockWave");
        }

        bool can = false;

        public override void StopSkill()
        {
            can = true;
            attackChargingPro.orthographicSize = 0.0f;
            attackChargingPro.gameObject.SetActive(false);
            attackRangePro.gameObject.SetActive(false);
        }
    }
}