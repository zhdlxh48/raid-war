using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public enum BTState
    {
        Success,
        Failure,
        Continue,
        Abort
    }

    public abstract class BTNode
    {
        public abstract BTState Tick();
    }

    public abstract class Branch : BTNode
    {
        protected int activeChild;
        protected List<BTNode> children = new List<BTNode>();

        public virtual Branch AddBranch(params BTNode[] children)
        {
            for (int i = 0; i < children.Length; i++)
            {
                this.children.Add(children[i]);
            }
            return this;
        }

        public virtual void ResetChildren()
        {
            activeChild = 0;
            for (int i = 0; i < children.Count; i++)
            {
                Branch b = children[i] as Branch;
                if (b != null)
                {
                    b.ResetChildren();
                }
            }
        }
    }

    public class Sequence : Branch
    {
        public override BTState Tick()
        {
            BTState state = children[activeChild].Tick();
            switch (state)
            {
                case BTState.Success:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;

                case BTState.Continue:
                    return BTState.Continue;

                case BTState.Failure:
                    activeChild = 0;
                    return BTState.Failure;

                case BTState.Abort:
                    activeChild = 0;
                    return BTState.Abort;
            }
            throw new System.Exception("Sequece Error");
        }
    }

    public abstract class ControlFlow : Branch
    {
        public override BTState Tick()
        {
            switch (children[activeChild].Tick())
            {
                case BTState.Continue:
                    return BTState.Continue;

                default:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BTState.Success;
                    }
                    return BTState.Continue;
            }
        }
    }

    public class Root : ControlFlow
    {
        public bool isTerminated = false;

        public override BTState Tick()
        {
            if (isTerminated) return BTState.Abort;
            while (true)
            {
                switch (children[activeChild].Tick())
                {
                    case BTState.Continue:
                        return BTState.Continue;
                    case BTState.Abort:
                        isTerminated = true;
                        return BTState.Abort;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BTState.Success;
                        }
                        continue;
                }
            }
        }
    }

    public class Repeat : ControlFlow
    {
        public int repeatCount = 1;
        private int curCount = 0;

        public Repeat(int repeatCount)
        {
            this.repeatCount = repeatCount;
        }

        public override BTState Tick()
        {
            if (repeatCount > 0 && curCount < repeatCount)
            {
                BTState result = base.Tick();
                switch (result)
                {
                    case BTState.Continue:
                        return BTState.Continue;

                    default:
                        curCount++;
                        if (curCount == repeatCount)
                        {
                            curCount = 0;
                            return BTState.Success;
                        }
                        return BTState.Continue;
                }
            }

            return BTState.Success;
        }
    }

    public class ConditionalBranch : ControlFlow
    {
        public System.Func<bool> func;
        bool tested = false;

        public ConditionalBranch(System.Func<bool> func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
           // Debug.Log($"ConditionStart: {func.ToString().ToString()}");
            if (!tested)
                tested = func();
            if (tested)
            {
                var result = base.Tick();
                if (result == BTState.Continue)
                    return BTState.Continue;
                else
                {
                    tested = false;
                    return result;
                }
            }
            else
            {
                return BTState.Failure;
            }
        }
    }

    public class Condition : BTNode
    {
        public System.Func<bool> func;

        public Condition(System.Func<bool> func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
            if (func())
                return BTState.Success;
            else
                return BTState.Failure;
        }
    }

    public class Action : BTNode
    {
        System.Action func;

        public Action(System.Action func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
            if (func != null)
            {
                func();
                return BTState.Success;
            }

            else
                return BTState.Continue;
        }
    }

    public class SetTrigger : BTNode
    {
        Animator anim;
        int hashId;
        //string triggerName;
        bool set = true;

        public SetTrigger(Animator anim, string para, bool set = true)
        {
            this.anim = anim;
            this.hashId = Animator.StringToHash(para);
           // this.triggerName = para;
            this.set = set;
        }

        public override BTState Tick()
        {
            if (set)
                anim.SetTrigger(hashId);
            else
                anim.ResetTrigger(hashId);

            return BTState.Success;
        }
    }

    public class SetBool : BTNode
    {
        Animator anim;
        int hashId;
        //string triggerName;
        bool set = true;

        public SetBool(Animator anim, string para, bool set = true)
        {
            this.anim = anim;
            this.hashId = Animator.StringToHash(para);
            //this.triggerName = para;
            this.set = set;
        }

        public override BTState Tick()
        {
            anim.SetBool(hashId, set);
            return BTState.Success;
        }
    }

    public class WaitAnimationAnd : BTNode
    {
        Animator anim;
        int hashId;

        public WaitAnimationAnd(Animator anim, string para)
        {
            this.anim = anim;
            this.hashId = Animator.StringToHash(para);
        }

        public override BTState Tick()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                return BTState.Success;
            }
            else
                return BTState.Continue;
        }
    }

    public class While : ControlFlow
    {
        public System.Func<bool> func;

        public While(System.Func<bool> func)
        {
            this.func = func;
        }

        public override BTState Tick()
        {
            if (func())
                base.Tick();
            else
            {
                //if we exit the loop
                ResetChildren();
                return BTState.Failure;
            }

            return BTState.Continue;
        }
    }

    public class Wait : BTNode
    {
        public float sec = 0;
        float sumSec = 0;

        public Wait(float sec)
        {
            this.sec = sec;
        }

        public override BTState Tick()
        {
            if (sumSec < sec)
            {
                sumSec += Time.deltaTime;
                return BTState.Continue;
            }
            else
            {
                sumSec = 0;
                return BTState.Success;
            }
        }
    }

    public class Abort : BTNode
    {

        public override BTState Tick()
        {
            return BTState.Abort;
        }

    }
}