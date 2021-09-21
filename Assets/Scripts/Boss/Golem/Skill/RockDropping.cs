using System.Collections;
using UnityEngine;

namespace Boss
{
    public class RockDropping : ProjectileObjectBase
    {
        public Projector attackChargingPro;
        public Projector attackRangePro;

        public ParticleSystem paticle;

        public Transform rock;

        private float orSizePerSec = 0.0f;
        private float rockStartY = 0.0f;
        // Start is called before the first frame update
        void OnEnable()
        {
            attackChargingPro.orthographicSize = 0.0f;
            attackRangePro.orthographicSize = attackRange;
            rock.GetComponent<SphereCollider>().radius = attackRange;
            orSizePerSec = (attackRange / activeTime);
            rockStartY = (fallSpeed * activeTime);
            rock.position = new Vector3(rock.position.x, rockStartY, rock.position.z);
            //paticle.time = 0.5f;
            StartCoroutine(Back());
        }

        IEnumerator Back()
        {
            while (rock.position.y >= 0.0f)
            {
                attackChargingPro.orthographicSize += orSizePerSec * Time.deltaTime;

                if (attackChargingPro.orthographicSize >= attackRange)
                {
                    attackChargingPro.orthographicSize = attackRange;
                }
                rock.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                yield return null;
            }

            attackChargingPro.gameObject.SetActive(false);
            attackRangePro.gameObject.SetActive(false);
            paticle.Play(true);
            yield return null;

            while (paticle.time < 0.8)
            {
                yield return null;
            }
            this.gameObject.SetActive(false);
        }
    }
}