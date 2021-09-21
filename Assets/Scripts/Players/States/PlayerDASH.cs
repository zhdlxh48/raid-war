using UnityEngine;
using System.Collections;

public class PlayerDASH : PlayerFSMState
{
    public float dashPower = 10.0f;
    Vector3 dir;

    private void OnEnable()
    {
        StartCoroutine(DashMove());
    }

    private IEnumerator DashMove()
    {
        if (GameKey.GetKeys(GameKey.moveKeys))
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.z = Input.GetAxis("Vertical");
            if (Mathf.Abs(dir.x) > 0.0f && Mathf.Abs(dir.z) > 0.0f)
            {
                dir *= 0.5f;
                Debug.Log(dir);
            }
        }

        TimerUtil.TimerReset(manager.timeManager.dashTimer);
        manager.rigid.velocity = Vector3.zero;
        manager.transf.rotation = Quaternion.LookRotation(dir);
        MovementUtil.ForceDashMove(manager.rigid, manager.transf, dir, dashPower, ForceMode.Impulse);
        manager.visualManager.PlayStateAnim(PlayableCharacterState.DASH);
        manager.visualManager.ActiveEffect(EffectOffset.DASH);

        yield return new WaitWhile(() => manager.visualManager.isAnimating);

        FSMNextState();
    }

    public override void FSMNextState()
    {
        if (GameKey.GetKeys(GameKey.moveKeys))
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        else
            manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}