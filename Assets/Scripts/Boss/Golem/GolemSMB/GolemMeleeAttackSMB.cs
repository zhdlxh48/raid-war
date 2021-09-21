// 작성자: 임자현
// 작성일: 2019-09-06
// 설명: 골램 근접 공격

using UnityEngine;
using System.Diagnostics;

namespace Boss
{
    public class GolemMeleeAttackSMB : MovingObjectSMB<GolemBehavior>
    {
        public override void OnUdate(Animator animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.58f)
            {
                monoBehavior.MeleeAttack();
            }
        }

        public override void OnExit(Animator animator)
        {
            monoBehavior.attackFlag = true;
        }
    }
}
