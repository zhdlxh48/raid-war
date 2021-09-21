// 작성자: 임자현
// 작성일: 2019-09-06
// 설명: 골렘 추적 상테

using UnityEngine;

namespace Boss
{
    public class GolemTraceSMB : MovingObjectSMB<GolemBehavior>
    {
        public override void OnUdate(Animator animator)
        {
            monoBehavior.TracingClosePlayer();
        }
    }
}
