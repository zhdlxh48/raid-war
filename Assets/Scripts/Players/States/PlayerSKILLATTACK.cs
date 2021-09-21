using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSKILLATTACK : PlayerFSMState
{
    private void OnEnable()
    {
        StartCoroutine(PlayAnimation());
    }

    public IEnumerator PlayAnimation()
    {
        TimerUtil.TimerReset(manager.timeManager.skillAttackTimers[0]);

        manager.visualManager.PlayStateAnim(PlayableCharacterState.SKILLATTACK);
        //manager.visualManager.ActiveEffect(EffectOffset.DASH);

        yield return new WaitForEndOfFrame();

        //Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / Animation Start Waiting");
        yield return new WaitUntil(() => manager.visualManager.isAnimating);
        //Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / Animation Playing");
        //
        yield return new WaitWhile(() => manager.visualManager.isAnimating);
        //Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / End Animation");

        FSMNextState();
    }

    public override void FSMNextState()
    {
        Debug.Log("Next");
        manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}
