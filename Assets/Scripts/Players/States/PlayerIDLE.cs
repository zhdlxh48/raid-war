using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class PlayerIDLE : PlayerFSMState
{
    private void OnEnable()
    {
        manager.visualManager.PlayStateAnim(PlayableCharacterState.IDLE);
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        FSMNextState();
    }

    public override void FSMNextState()
    {
        if (GameKey.GetKeyDown(GameKeyPreset.NormalAttack))
        {
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        if (GameKey.GetKeyDown(GameKeyPreset.Skill_1))
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
        if (GameKey.GetKeys(GameKey.moveKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        }
        if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
            if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
                manager.SetPlayerState(PlayableCharacterState.DASH);
        }
    }
}
