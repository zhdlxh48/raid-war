using UnityEngine;
using System.Collections;

public class PlayerTimerManager : MonoBehaviour
{
    public GameTimer normalAttackTimer;
    public GameTimer[] skillAttackTimers;

    public GameTimer dashTimer;

    public GameTimer[] itemUseTimer;

    PlayerTimerManager()
    {
        skillAttackTimers = new GameTimer[1];
        itemUseTimer = new GameTimer[6];
    }

    public void TimerUpdate()
    {
        // Normal Attack Delay Timer
        TimerUtil.TimerCyclePlay(normalAttackTimer);
        // Skill Cool Timer
        if (skillAttackTimers.Length > 0)
        {
            foreach (var item in skillAttackTimers)
            {
                TimerUtil.TimerCyclePlay(item);
            }
        }

        // Dash Timer
        TimerUtil.TimerCyclePlay(dashTimer);

        // Item Use Timer
        if (itemUseTimer.Length > 0)
        {
            foreach (var item in itemUseTimer)
            {
                TimerUtil.TimerCyclePlay(item);
            }
        }
    }
}
