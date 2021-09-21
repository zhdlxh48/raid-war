using UnityEngine;
using System.Collections;

public enum EffectOffset { HIT, DASH }

[RequireComponent(typeof(Animator))]
public class PlayerVisualManager : MonoBehaviour
{
    private PlayerFSMManager manager;

    public Animator anim;

    private WaitForEndOfFrame waitAnimFrame;
    public bool isAnimating = false;

    #region Animator State Hash

    public readonly int hashState = Animator.StringToHash("state");
    public readonly int hashAttackLink = Animator.StringToHash("attackLink");
    public readonly int hashDamage = Animator.StringToHash("damage");
    public readonly int hashDead = Animator.StringToHash("dead");

    #endregion

    public GameObject[] effectPreset;
    WaitForSeconds effectWaitTime = new WaitForSeconds(0.3f);

    private void Awake()
    {
        manager = GetComponentInParent<PlayerFSMManager>();

        anim = GetComponent<Animator>();

        waitAnimFrame = new WaitForEndOfFrame();
    }

    public void InitEffects()
    {
        foreach (var item in effectPreset)
        {
            item.SetActive(false);
            item.hideFlags = HideFlags.HideInHierarchy;
        }
    }

    public void ActiveEffect(EffectOffset offset)
    {
        GameObject targEffect = effectPreset[(int)offset];

        StartCoroutine(EffectCoroutine(targEffect));
    }
    private IEnumerator EffectCoroutine(GameObject targEffectObj)
    {
        targEffectObj.SetActive(true);
        yield return effectWaitTime;
        targEffectObj.SetActive(false);
    }

    public void PlayStateAnim(PlayableCharacterState enumState)
    {
        StartCoroutine(AnimCoroutine(hashState, (int)enumState));
    }
    public void PlayLinkAnim()
    {
        StartCoroutine(AnimCoroutine(hashAttackLink));
    }
    public void PlayDamageAnim(bool isDown)
    {
        if (!isDown)
            StartCoroutine(AnimCoroutine(hashState, (int)PlayableCharacterState.DAMAGE));
        else
            StartCoroutine(AnimCoroutine(hashState, (int)PlayableCharacterState.DAMAGE_DOWN));

        StartCoroutine(AnimCoroutine(hashDamage));
    }
    public void PlayDeadAnim()
    {
        StartCoroutine(AnimCoroutine(hashDead));
    }

    #region Animation Coroutine

    private IEnumerator AnimCoroutine(int hash)
    {
        anim.SetTrigger(hash);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, bool isEnable)
    {
        anim.SetTrigger(hash);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, int value)
    {
        anim.SetInteger(hash, value);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, float value)
    {
        anim.SetFloat(hash, value);
        yield return waitAnimFrame;
    }

    #endregion

    #region Animation Event

    public void StartAnimationSection()
    {
       // Debug.Log("[" + GetType().Name + "] " + "Animation Start");
        isAnimating = true;
    }

    public void EndAnimationSection()
    {
      // Debug.Log("[" + GetType().Name + "] " + "Animation End");
        isAnimating = false;
    }

    public void ApplyNormalAttackInAnim()
    {
        //Debug.Log("공격 적용 In 애니메이션");
        StartCoroutine(manager.skillManager.NormalAttack());
    }

    public void ApplySkillAttackDamageInAnim()
    {
        StartCoroutine(manager.skillManager.SkillAttack(false));
    }

    public void ApplySkillAttackCriticalInAnim()
    {
        StartCoroutine(manager.skillManager.SkillAttack(true));
    }

    #endregion
}