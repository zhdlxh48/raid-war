using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitchableCollection
{
    public bool isMovable;
    public bool isAttackable;
}

public class PlayerFSMState : MonoBehaviour
{
    protected PlayerFSMManager manager;

    public StateSwitchableCollection stateSwitch;

    public string stateName;

    private void Awake()
    {
        manager = GetComponent<PlayerFSMManager>();
    }

    public virtual void FSMStart() { }
    public virtual void FSMUpdate() { }
    public virtual void FSMFixedUpdate() { }
    public virtual void FSMNextState() { }
}
