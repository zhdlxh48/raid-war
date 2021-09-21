// 작성자: 임자현
// 작성일: 2019-09-07
// 설명: 투사체 베이스

using UnityEngine;
using System.Collections.Generic;

namespace Boss
{
    public class ProjectileObjectBase : MonoBehaviour
    {
        protected float attackRange = 1; // 원형 공격 반경
        protected float fallSpeed = 50.0f; // 투사체 속도
        protected float activeTime = 2;
        protected int minDamage; 
        protected int maxDamage; 

        private Vector3 impactPoint; // 탄착 지점

        public void Initialize(float range, float fallSpeed, float activeTime, int minDamage, int maxDamage)
        {
            this.attackRange = range;
            this.fallSpeed = fallSpeed;
            this.activeTime = activeTime;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }

        public void SetImpactPoint(Vector3 ImpactPoint)
        {
            this.impactPoint = ImpactPoint;
        }
    }
}