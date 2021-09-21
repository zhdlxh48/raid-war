using UnityEngine;
using System;
using System.Collections;

public enum SkillType { TYPE_DAMAGE, TYPE_BUFF, TYPE_DEBUFF }
public enum SkillRare { RARE_NORMAL, RARE_ULTIMATE }
public enum SkillTarget { PLAYER_OWN = 1, PLAYER_OTHER = 2, ENEMY = 4, ONLY_BOSS = 8 }
public enum SkillGauge { NORMAL_GAUGE, SKILL_GAUGE }
public enum SkillCCType {  }

[Serializable]
public class SkillData
{
    [SerializeField] private int skillIndex;
    [SerializeField] private string skillName;
    // TODO: 스킬 사용 직업
    // 0: Normal, 1: Ultimate
    [SerializeField] private int skillRare;
    [SerializeField] private int skillSlot;
    // 0: Active, 1: Passive
    [SerializeField] private int skillApplyType;
    [SerializeField] private SkillTarget skillApplyTarget;
    [SerializeField] private float skillApplyRange;

    [SerializeField] private SkillType skillType;
    // TODO: 스킬 세부타입
    [SerializeField] private float skillEffectPower;
    // If time is 0, Immediately cast
    [SerializeField] private float skillCastTime;

    [SerializeField] private float skillUseMana;
    [SerializeField] private float skillReCastTime;
    // TODO: 사정거리 (스킬 적용 거리와 무슨 차이..?)

    [SerializeField] private bool canSkillCancel;
    [SerializeField] private SkillCCType skillCCType;
    [SerializeField] private float skillCCEffectPower;
    [SerializeField] private float skillCCEffectTime;

    public int SkillIndex { get => skillIndex; set => skillIndex = value; }
    public string SkillName { get => skillName; set => skillName = value; }
    public int SkillRare { get => skillRare; set => skillRare = value; }
    public int SkillSlot { get => skillSlot; set => skillSlot = value; }
    public int SkillApplyType { get => skillApplyType; set => skillApplyType = value; }
    public SkillTarget SkillApplyTarget { get => skillApplyTarget; set => skillApplyTarget = value; }
    public float SkillApplyRange { get => skillApplyRange; set => skillApplyRange = value; }
    public SkillType SkillType { get => skillType; set => skillType = value; }
    public float SkillEffectPower { get => skillEffectPower; set => skillEffectPower = value; }
    public float SkillCastTime { get => skillCastTime; set => skillCastTime = value; }
    public float SkillUseMana { get => skillUseMana; set => skillUseMana = value; }
    public float SkillReCastTime { get => skillReCastTime; set => skillReCastTime = value; }
    public bool CanSkillCancel { get => canSkillCancel; set => canSkillCancel = value; }
    public SkillCCType SkillCCType { get => skillCCType; set => skillCCType = value; }
    public float SkillCCEffectPower { get => skillCCEffectPower; set => skillCCEffectPower = value; }
    public float SkillCCEffectTime { get => skillCCEffectTime; set => skillCCEffectTime = value; }
}
