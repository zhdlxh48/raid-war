// 작성자: 임자현
// 작성일: 2019-09-06
// 설명: 공격 패턴 

using UnityEngine;

// 패턴 공격 베이스 클래스
public class AttackActionBase : MonoBehaviour
{
    public int skillFactor = 0;
    public float attackRange = 1;

    public virtual void ExcuteSkill(int damage) { }
    public virtual void StopSkill() { }
}
