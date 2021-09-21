using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StatusFixer : MonoBehaviour
{
    // Variables
    [SerializeField] private float strength;
    [SerializeField] private float dexterity;
    [SerializeField] private float intellect;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float attackMinPower;
    [SerializeField] private float attackMaxPower;

    [SerializeField] private float attackSpeed;

    [SerializeField] private float health;
    [SerializeField] private float magicPoint;

    [SerializeField] private float armor;
    [SerializeField] private float avoidRate;

    // Properties
    /// <summary> 힘 </summary>
    public float Strength
    {
        get => strength;
        set => strength = value;
    }
    /// <summary> 민첩성 </summary>
    public float Dexterity
    {
        get => dexterity;
        set => dexterity = value;
    }
    /// <summary> 지능 </summary>
    public float Intellect
    {
        get => intellect;
        set => intellect = value;
    }

    /// <summary> 이동속도 </summary>
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    /// <summary> 최소 공격력 </summary>
    public float AttackMinPower
    {
        get => attackMinPower;
        set => attackMinPower = value;
    }
    /// <summary> 최대 공격력 </summary>
    public float AttackMaxPower
    {
        get => attackMaxPower;
        set => attackMaxPower = value;
    }

    /// <summary> 공격 속도 </summary>
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    /// <summary> 체력 </summary>
    public float Health
    {
        get => health;
        set => health = value;
    }
    /// <summary> 마나 </summary>
    public float MagicPoint
    {
        get => magicPoint;
        set => magicPoint = value;
    }

    /// <summary> 방어력 </summary>
    public float Armor
    {
        get => armor;
        set => armor = value;
    }
    /// <summary> 회피 확률 </summary>
    public float AvoidRate
    {
        get => avoidRate;
        set => avoidRate = value;
    }
}
