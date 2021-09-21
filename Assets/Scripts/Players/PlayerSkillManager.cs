using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Boss;

public class PlayerSkillManager : MonoBehaviour
{
    private PlayerFSMManager manager;
    public UnityEvent atkCam;
    //public AudioClip sword_f;

    //public Damageable onBossDamage;

    public SkillData normalAttackData;

    public BossMonsterBase[] skillTargetBoss;

    public int normalSkillStack;
    public bool isLinkToNext;

    private void Awake()
    {
        manager = GetComponent<PlayerFSMManager>();

        normalAttackData = new SkillData();
    }

    public IEnumerator NormalAttack()
    {
        if (skillTargetBoss != null)
        {
            // 크리티컬 판정
            foreach (var item in skillTargetBoss)
            {
                // TODO: item에 데미지를 가한다
                // 데미지 : manager.statusManager.GetFinalAttackDamage();
                atkCam.Invoke();
                // Core.SoundManager.OneShot(sword_f);
                manager.visualManager.ActiveEffect(EffectOffset.HIT);
                item.OnDamage((int)manager.statusManager.GetFinalAttackDamage());
                //  Debug.Log("데미지 적용");
                // Debug.Log("데미지 : " + manager.statusManager.GetFinalAttackDamage());
                //onBossDamage.Invoke((int)manager.statusManager.GetFinalAttackDamage());
            }
        }

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator SkillAttack(bool isCritical)
    {
        if (skillTargetBoss != null)
        {
            foreach (var item in skillTargetBoss)
            {
                item.isAwake = false;
                // TODO: item에 데미지를 가한다
                // 데미지 : manager.statusManager.GetFinalAttackDamage();
                atkCam.Invoke();
                // Core.SoundManager.OneShot(sword_f);
                manager.visualManager.ActiveEffect(EffectOffset.HIT);
                if (isCritical)
                    item.OnDamage((int)manager.statusManager.GetCriticalDamage());
                else
                    item.OnDamage((int)manager.statusManager.GetFinalAttackDamage());
                //  Debug.Log("데미지 적용");
                // Debug.Log("데미지 : " + manager.statusManager.GetFinalAttackDamage());
                //onBossDamage.Invoke((int)manager.statusManager.GetFinalAttackDamage());
            }
        }

        yield return new WaitForEndOfFrame();
    }
}
