using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerUtil
{
    public static void TimerCyclePlay(GameTimer timer)
    {
        if (timer.timer < timer.coolTime)
        {
            timer.timer += Time.deltaTime;
        }
        else
        {
            if (timer.timer != timer.coolTime)
            {
                timer.timer = timer.coolTime;
            }
        }
    }

    public static bool IsOnCoolTime(GameTimer timer)
    {
        return timer.timer < timer.coolTime;
    }

    public static void TimerReset(GameTimer timer)
    {
        timer.timer = 0.0f;
    }
}
