using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockDrop : AttackActionBase
    {
        public GameObject rockOb;
        public Transform target;

        private GolemBehavior golem;

        private GameObject rock;  
        // Start is called before the first frame update
        void Awake()
        {
            golem = transform.root.GetComponent<GolemBehavior>();
            rock = Instantiate(rockOb);
            rock.hideFlags = HideFlags.HideInHierarchy;
            rock.SetActive(false);
        }

        public override void ExcuteSkill(int damage)
        {
            if (!rock.gameObject.activeSelf)
            {
                Vector3 temp = target.position;
                temp.y = 30.0f;
                rock.GetComponentInChildren<RockRockScript>().SetRockInfo(golem, damage * skillFactor);
                rock.transform.position = temp;
                rock.SetActive(true);
            }
        }
    }
}