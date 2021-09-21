using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class GameTimer
{
    public float coolTime;
    public float timer = 0.0f;

    public GameTimer(float cooltime)
    {
        coolTime = cooltime;
        //timer = cooltime;
    }
}
