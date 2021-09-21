// 작성자: 임자현
// 작성일: 2019-09-05
// 설명: 스테이트 머신 비헤이비어 베이스

using UnityEngine;
using UnityEngine.Animations;

namespace Boss
{

    public class MovingObjectSMB<TMonoBehavior> : SealedSMB
        where TMonoBehavior : MonoBehaviour
    {
        protected TMonoBehavior monoBehavior;

        public static void Initialize(Animator anim, TMonoBehavior behavior)
        {
            MovingObjectSMB<TMonoBehavior>[] smb = anim.GetBehaviours<MovingObjectSMB<TMonoBehavior>>();

            for (int i = 0; i < smb.Length; i++)
            {
                smb[i].InternalInitialise(anim, behavior);
            }
        }

        protected void InternalInitialise(Animator animator, TMonoBehavior MonoBehaviour)
        {
            monoBehavior = MonoBehaviour;
            OnStart(animator);
        }

        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            OnEnter(animator);
        }

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            OnUdate(animator);
        }

        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            OnExit(animator);
        }

        public virtual void OnStart(Animator animator) { }

        public virtual void OnEnter(Animator animator) { }

        public virtual void OnUdate(Animator animator) { }

        public virtual void OnExit(Animator animator) { }
    }

    public abstract class SealedSMB : StateMachineBehaviour
    {
        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}
