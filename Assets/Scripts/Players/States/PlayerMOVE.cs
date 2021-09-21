using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class PlayerMOVE : PlayerFSMState
{
    private float horizMoveValue = 0.0f;
    private float vertMoveValue = 0.0f;

    private float rotateSpeed = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void OnEnable()
    {
        manager.visualManager.PlayStateAnim(PlayableCharacterState.MOVE);
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        InputValueUpdate();
        FSMNextState();
    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();

        MoveRotate();
    }

    public override void FSMNextState()
    {
        if (GameKey.GetKeyDown(GameKeyPreset.NormalAttack))
        {
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        else if (GameKey.GetKeyDown(GameKeyPreset.Skill_1))
        {
            if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[0]))
            {
                BossMonsterBase[] tempAttackBoss = BossUtil.GetBossComponents(DetectUtil.DetectObjectsTransformWithAngle(BossUtil.GetBossLocations(manager.boss), manager.transf, 360.0f, 12.0f));

                if (tempAttackBoss.Length != 0)
                {
                    manager.skillManager.skillTargetBoss = tempAttackBoss;
                    manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
                }
                else
                {
                    manager.skillManager.skillTargetBoss = null;
                }
            }
        }
        else if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
            if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
                manager.SetPlayerState(PlayableCharacterState.DASH);
        }
        else if (!Input.anyKey)
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }

    private void InputValueUpdate()
    {
        horizMoveValue = InputControlUtil.InputLeftRightValue();
        vertMoveValue = InputControlUtil.InputUpDownValue();

        moveDirection.x = horizMoveValue;
        moveDirection.z = vertMoveValue;
        moveDirection = moveDirection.normalized;
    }

    private void MoveRotate()
    {
        MovementUtil.VectorMove(manager.rigid, moveDirection, manager.statusManager.MoveSpeed);
        MovementUtil.Rotate(manager.transf, moveDirection, rotateSpeed);
    }
}
