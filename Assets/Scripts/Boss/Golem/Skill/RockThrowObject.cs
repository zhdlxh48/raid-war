using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockThrowObject : InteractCollision3D
    {
        GolemBehavior golem;
        int damage;
        public void SetRockInfo(GolemBehavior golem, int damage)
        {
            this.golem = golem;
            this.damage = damage;
        }

        public override void OnEnter(GameObject go)
        {
            golem.playerOnDamage.Invoke(damage);
        }
    }
}