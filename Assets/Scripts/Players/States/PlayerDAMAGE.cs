using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDAMAGE : PlayerFSMState
{
    private void OnEnable()
    {
        StartCoroutine(DamageAnim());
    }

    private IEnumerator DamageAnim()
    {
        manager.visualManager.PlayDamageAnim(false);

       // Debug.Log("AnimationWaiting");
       // Debug.Log("isAnimating : " + manager.animManager.isAnimating);
        yield return new WaitUntil(() => manager.visualManager.isAnimating);

        manager.isDamageable = false;
       // Debug.Log("isDamageable : " + manager.isDamageable);

       // Debug.Log("EndWaiting");
       // Debug.Log("isAnimating : " + manager.animManager.isAnimating);
        yield return new WaitWhile(() => manager.visualManager.isAnimating);

        manager.isDamageable = true;
        //Debug.Log("isDamageable : " + manager.isDamageable);

        //Debug.Log("EndAnimation");
       // Debug.Log("isAnimating : " + manager.animManager.isAnimating);
        
        //Debug.Log("Go To IDLE");
        FSMNextState();
    }

    public override void FSMNextState()
    {
        manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}
