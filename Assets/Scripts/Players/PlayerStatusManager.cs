using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;

[Serializable]
public class PlayerStatusManager : PlayerStatusData
{
    public StatusFixer skillFix;
    //public StatusFixer itemFix;

    public void InitAllStatus()
    {
        SetMoveSpeed();
        SetHealth().SetMagicPoint();
        SetAttackMinPower().SetAttackMaxPower();
        SetAttackSpeed();
        SetArmor().SetAvoidRate();
    }

    public PlayerStatusManager SetMoveSpeed()
    {
        MoveSpeed = (MoveSpeedByJob * (1.0f + (skillFix != null ? skillFix.MoveSpeed : 0.0f)));
        return this;
    }

    public PlayerStatusManager SetHealth()
    {
        Health = (100 + (25 * Strength) + (skillFix != null ? skillFix.Health : 0.0f)) * (1.0f + ((skillFix != null ? skillFix.Health : 0.0f) / 100.0f));
        return this;
    }

    public PlayerStatusManager SetMagicPoint()
    {
        MagicPoint = ((15.0f * Intellect) + (skillFix != null ? skillFix.MagicPoint : 0.0f)) * (1.0f + ((skillFix != null ? skillFix.MagicPoint : 0.0f) / 100.0f));
        return this;
    }

    public PlayerStatusManager SetAttackMinPower()
    {
        AttackMinPower = ((MainCharAttackPower * 1.35f) + (skillFix != null ? skillFix.AttackMinPower : 0.0f)) * (1.0f + ((skillFix != null ? skillFix.AttackMinPower : 0.0f) / 100.0f));
        return this;
    }

    public PlayerStatusManager SetAttackMaxPower()
    {
        AttackMaxPower = ((MainCharAttackPower * 2.25f) + (skillFix != null ? skillFix.AttackMaxPower : 0.0f)) * (1.0f + ((skillFix != null ? skillFix.AttackMaxPower : 0.0f) / 100.0f));
        return this;
    }

    /// <summary> 최종 공격력 [Min: attackMinPower (inclusive) / Max: attackMaxPower (inclusive)] </summary>
    public float GetFinalAttackDamage()
    {
        return Random.Range(AttackMinPower, AttackMaxPower);
    }

    /// <summary> 치명타 피해 </summary>
    public float GetCriticalDamage()
    {
        return GetFinalAttackDamage() * Random.Range(1.5f, 2.0f);
    }

    public PlayerStatusManager SetAttackSpeed()
    {
        AttackSpeed = (MainCharAttackSpeed / (1.0f + (0.02f * Dexterity) + (skillFix != null ? skillFix.AttackSpeed : 0.0f)));
        return this;
    }

    public PlayerStatusManager SetArmor()
    {
        Armor = (-2.0f + (0.3f * Dexterity) + (skillFix != null ? skillFix.Armor : 0.0f)) * (1.0f + ((skillFix != null ? skillFix.Armor : 0.0f) / 100.0f));
        return this;
    }

    public PlayerStatusManager SetAvoidRate()
    {
        AvoidRate = (((0.25f * Dexterity) + (skillFix != null ? skillFix.AvoidRate : 0.0f)) * 0.01f);
        return this;
    }
}