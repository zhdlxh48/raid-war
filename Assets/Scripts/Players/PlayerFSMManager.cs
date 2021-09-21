using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;
using System;

public enum PlayableCharacterState
{
    IDLE, MOVE, DASH, NORMALATTACK, SKILLATTACK, DAMAGE, DAMAGE_DOWN, DEAD
}

public class PlayerFSMManager : MonoBehaviour
{
    // Manager Scripts
    public PlayerTimerManager timeManager;
    public PlayerStatusManager statusManager;
    public PlayerSkillManager skillManager;
    public PlayerVisualManager visualManager;

    // FSM Manage Variables
    private Dictionary<PlayableCharacterState, PlayerFSMState> playerStates = new Dictionary<PlayableCharacterState, PlayerFSMState>();

    public PlayableCharacterState startState;
    public PlayableCharacterState currentState;
    public PlayerFSMState currentFSMAction;

    // Player Component Variables
    public Transform transf;
    public Rigidbody rigid;

    // Boss Register
    public int bossNum;
    public BossMonsterBase[] boss;

    // Damage
    public bool isDamageable;

    private void Awake()
    {
        statusManager = GetComponent<PlayerStatusManager>();
        timeManager = GetComponent<PlayerTimerManager>();
        skillManager = GetComponent<PlayerSkillManager>();
        visualManager = GetComponentInChildren<PlayerVisualManager>();

        InitPlayerDefaultStatus();
        InitPlayerDefaultTimer();
        InitPlayerDefaultSkill();
        InitPlayerDefaultVisual();
        InitPlayerStates();

        transf = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        boss = FindObjectsOfType<BossMonsterBase>();
        bossNum = boss.Length;

        isDamageable = true;
    }

    private void Start()
    {
        // Awake에 넣으면 PlayerAnimatorManager에서 Animator를 불러오는 시기와 겹쳐 정상적으로 작동하지 않음
        SetPlayerState(startState);
    }

    public bool awaking = true;

    private void Update()
    {
        if (!awaking)
            return;

        currentFSMAction?.FSMUpdate();

        timeManager.TimerUpdate();

        // Item Use Check
        if (GameKey.GetKeys(GameKey.itemUseKeys))
        {
            ItemUse(GameKey.ReturnInputKey(GameKey.itemUseKeys));
        }

        //OnDead();

        //DEBUG
        //DetectUtil.DebugDrawAngle(BossUtil.GetBossLocations(boss), transf, 60.0f, 10.0f);
    }

    private void FixedUpdate()
    {
        if (!awaking)
            return;

        currentFSMAction?.FSMFixedUpdate();
    }

    /// <summary>
    /// Add
    /// </summary>
    public bool OnDead()
    {
        if (statusManager.Health <= 0)
        {
            SetPlayerState(PlayableCharacterState.DEAD);
            return true;
        }
        return false;
    }

    public void OnDamage(int damage)
    {
        if (!awaking)
            return;

        if (isDamageable)
        {
            int da = (int)((damage) * (1.0f - ((statusManager.Armor + statusManager.Strength) * 0.001f))) - (int)statusManager.Armor;
            SprinkleDamageText.OnDamageText(DamageType.Nomal, HitActor.Player, transf.position, da);
            statusManager.Health -= da;

            if (statusManager.Health > 0) SetPlayerState(PlayableCharacterState.DAMAGE);
        }
    }

    public void OnDamageDown(int damage, Vector3 dir)
    {
        if (!awaking)
            return;

        if (isDamageable)
        {
            int da = (int)((damage) * (1.0f - ((statusManager.Armor + statusManager.Strength) * 0.001f))) - (int)statusManager.Armor;
            SprinkleDamageText.OnDamageText(DamageType.Nomal, HitActor.Player, transf.position, da);
            statusManager.Health -= da;

            if (statusManager.Health > 0)
            {
                SetPlayerState(PlayableCharacterState.DAMAGE_DOWN);
                RigHit(dir);
            }
        }
    }

    public void RigHit(Vector3 dir)
    {
        dir.y += 2.0f;

        rigid.AddForce(dir * 1.9f, ForceMode.Impulse);
    }

    /// <summary>
    /// Add
    /// </summary>

    #region Init States

    private void InitPlayerDefaultStatus()
    {
        statusManager.Strength = 50.0f;
        statusManager.Dexterity = 25.0f;
        statusManager.Intellect = 10.0f;

        statusManager.MoveSpeedByJob = 5.0f;
        statusManager.MainCharAttackPower = 35.0f;
        statusManager.MainCharAttackSpeed = 5.0f;

        statusManager.Health = 100.0f;
        statusManager.MagicPoint = 40.0f;

        statusManager?.InitAllStatus();
    }
    private void InitPlayerDefaultTimer()
    {
        timeManager.normalAttackTimer.coolTime = 1.0f;

        timeManager.skillAttackTimers[0].coolTime = 45.0f;

        timeManager.dashTimer.coolTime = 2.0f;

        // TODO: 추후에 ITEM MANAGE CLASS에서 정보 불러오기
        timeManager.itemUseTimer[0].coolTime = 1.0f;
        timeManager.itemUseTimer[1].coolTime = 1.0f;
        timeManager.itemUseTimer[2].coolTime = 1.0f;
        timeManager.itemUseTimer[3].coolTime = 1.0f;
        timeManager.itemUseTimer[4].coolTime = 1.0f;
        timeManager.itemUseTimer[5].coolTime = 1.0f;
    }
    private void InitPlayerDefaultSkill()
    {
        // Normal Attack Info
        skillManager.normalSkillStack = 0;
    }
    private void InitPlayerDefaultVisual()
    {
        visualManager.InitEffects();
    }
    private void InitPlayerStates()
    {
        playerStates[PlayableCharacterState.IDLE] = GetComponent<PlayerIDLE>();
        playerStates[PlayableCharacterState.MOVE] = GetComponent<PlayerMOVE>();
        playerStates[PlayableCharacterState.DASH] = GetComponent<PlayerDASH>();
        playerStates[PlayableCharacterState.NORMALATTACK] = GetComponent<PlayerNORMALATTACK>();
        playerStates[PlayableCharacterState.SKILLATTACK] = GetComponent<PlayerSKILLATTACK>();
        playerStates[PlayableCharacterState.DAMAGE] = GetComponent<PlayerDAMAGE>();
        playerStates[PlayableCharacterState.DAMAGE_DOWN] = GetComponent<PlayerDAMAGEDOWN>();
        playerStates[PlayableCharacterState.DEAD] = GetComponent<PlayerDEAD>();

        startState = PlayableCharacterState.IDLE;
        currentState = startState;
    }

    #endregion

    public void SetPlayerState(PlayableCharacterState stat)
    {
        foreach (var item in playerStates)
        {
            item.Value.enabled = false;
        }
        currentState = stat;
        playerStates[stat].enabled = true;

        currentFSMAction = playerStates[stat];
        currentFSMAction.FSMStart();
    }

    public void ItemUse(GameKeyPreset itemKey)
    {
        switch (itemKey)
        {
            case GameKeyPreset.ITEM_1:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[0]))
                {
                    Debug.Log("Item 1 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[0]);
                } 
                break;
            case GameKeyPreset.ITEM_2:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[1]))
                {
                    Debug.Log("Item 2 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[1]);
                }
                break;
            case GameKeyPreset.ITEM_3:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[2]))
                {
                    Debug.Log("Item 3 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[2]);
                }
                break;
            case GameKeyPreset.ITEM_4:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[3]))
                {
                    Debug.Log("Item 4 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[3]);
                }
                break;
            case GameKeyPreset.ITEM_5:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[4]))
                {
                    Debug.Log("Item 5 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[4]);
                }
                break;
            case GameKeyPreset.ITEM_6:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[5]))
                {
                    Debug.Log("Item 6 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[5]);
                }
                break;
            default:
                break;
        }
    }
}
