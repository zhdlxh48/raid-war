using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Boss
{
    public enum GolemSkill
    {
        Earthquake,
        RushAttack,
        RockDrop,
        ShockWave,
        RockfallRain,
        RockThrow
    };

    public class BossPattern
    {
        public int nextPatternHp;
        public int[] weight;
        public GolemSkill[] skillKinds;
    }

    public class GolemBehavior : BossMonsterBase
    {
        public int phase = 1;
        public float excuteSkillTime = 1.5f;
        public float idleWaitTime = 1.5f;
        public float castingRisesTime = 3.0f;
        public float attackCastingRises = 0.1f;
        public float jointExceptDistance = 10.0f;
        public Transform[] playerTransforms; // 유저들의 위치
        public UnityEngine.Events.UnityEvent camShake;
        public Damageable playerOnDamage;
        public DamageableRig playerOnDamageRig;

        // Hide
        public Transform trans;
        public Transform closePlayerTrans; // 가까운 유저의 위치
        public bool attackFlag = true;

        private float angleRange = 160f;
        private float distance = 3.6f;
        private BossPattern[] patterns;
        private GolemSkill curSkill;
        private GolemSkill select;
        private Dictionary<GolemSkill, AttackActionBase> skills;
        private float castingGauge = 0.0f;
        private Animator anim;
        private Root ai;
        private Vector3 originPos;

        private void Awake()
        {
            trans = GetComponent<Transform>();
            anim = GetComponentInChildren<Animator>();
            stats = GetComponent<BossStat>();

            originPos = trans.position;

            stats.RefreshAllStats(startSTR, startDEX, startINT, startLevel, moveSpeed);
            curHP = stats.HP;

            playerOnDamage.AddListener(playerTransforms[0].GetComponent<PlayerFSMManager>().OnDamage);
            playerOnDamageRig.AddListener(playerTransforms[0].GetComponent<PlayerFSMManager>().OnDamageDown);
            //playerOnDamage = playerTransforms[0].GetComponent

            MovingObjectSMB<GolemBehavior>.Initialize(anim, this);

            // 보스 스킬 추가
            skills = new Dictionary<GolemSkill, AttackActionBase>();
            skills.Add(GolemSkill.Earthquake, GetComponentInChildren<Earthquake>());
            skills.Add(GolemSkill.RushAttack, GetComponentInChildren<RushAttack>());
            skills.Add(GolemSkill.RockDrop, GetComponentInChildren<RockDrop>());
            skills.Add(GolemSkill.ShockWave, GetComponentInChildren<ShockWave>());
            skills.Add(GolemSkill.RockfallRain, GetComponentInChildren<RockfallRain>());
            skills.Add(GolemSkill.RockThrow, GetComponentInChildren<RockThrow>());

            #region Pattern
            patterns = new BossPattern[3];
            BossPattern phase = new BossPattern();

            phase.weight = new int[] { 3, 5, 8, 10 };
            // 수정 필요
            phase.skillKinds = new GolemSkill[] { GolemSkill.Earthquake, GolemSkill.RushAttack, GolemSkill.RockDrop, GolemSkill.ShockWave };
            phase.nextPatternHp = stats.HP / 2;
            patterns[0] = phase;

            phase = new BossPattern();
            phase.weight = new int[] { 2, 4, 7, 10 };
            phase.skillKinds = new GolemSkill[] { GolemSkill.RushAttack, GolemSkill.ShockWave, GolemSkill.RockfallRain, GolemSkill.RockThrow };
            phase.nextPatternHp = stats.HP / 5;
            patterns[1] = phase;

            phase = new BossPattern();
            phase.weight = new int[] { 1, 2, 6, 10 };
            phase.skillKinds = new GolemSkill[] { GolemSkill.RushAttack, GolemSkill.ShockWave, GolemSkill.RockfallRain, GolemSkill.RockThrow };
            patterns[2] = phase;
            #endregion

            #region Behavior Tree
            ai = new Root();
            ai.AddBranch(
                new SetTrigger(anim, "Skill"),
                new Wait(1.2f),
                new Action(StartSound),
                new WaitAnimationAnd(anim, "Idle"),
                new Wait(0.3f),
                new Action(ClosePlayerResearch),
                new Repeat(patterns.Length).AddBranch(

                    new While(ReplacePattern).AddBranch(

                        new While(IsAwake).AddBranch(
                            new ConditionalBranch(CastingSuccess).AddBranch(
                                new SetBool(anim, "Move", false),
                                new Wait(excuteSkillTime),
                                new Action(RandomSkillSelect),
                                new SetTrigger(anim, "Skill"),
                                new Action(ExcuteSkill),
                                new WaitAnimationAnd(anim, "Idle"),
                                new Wait(idleWaitTime),
                                new Action(ClosePlayerResearch),
                                new Action(ResetCastingGage)
                                ),

                            new SetBool(anim, "Move"),

                            new ConditionalBranch(MeleeAttackCheak).AddBranch(
                                new SetBool(anim, "Move", false),
                                new SetTrigger(anim, "MeleeAttack"),
                                new WaitAnimationAnd(anim, "Idle"),
                                new Wait(idleWaitTime),
                                new Action(ClosePlayerResearch)
                            )
                        ),
                        new Action(StopSkill),
                        new SetTrigger(anim, "Skill", false),
                        new SetTrigger(anim, "Move", false),
                        new SetTrigger(anim, "Grogi"),
                        new Wait(1.0f),
                        new WaitAnimationAnd(anim, "Idle"),
                        new Action(Awaking)
                    ),
                    new SetTrigger(anim, "Skill", false),
                    new SetTrigger(anim, "MeleeAttack", false),
                    new SetTrigger(anim, "Grogi", false),
                    new SetTrigger(anim, "Move", false),
                    new Wait(1.0f)
                ),
                new Action(BossEnd),
                new SetTrigger(anim, "Dead"),
                new Abort()
            );
            #endregion
        }

        private void Update()
        {
            if (isGame)
            {
                ai.Tick();
                CastingSuccess();
            }
        }

        // 페이즈 제설정
        public bool ReplacePattern()
        {
            if (OnDead() || GameManager.CurState == GameManager.GameState.Defeat)
                return false;

            if (curHP <= patterns[phase - 1].nextPatternHp)
            {
                if (phase == 2)
                {
                    stats.RefreshAllStats(15, 15, 15, 1, moveSpeed);
                }

                phase++;
                return false;
            }

            return true;
        }

        public void ExcuteSkill()
        {
            skills[curSkill].ExcuteSkill(CalDamage());
        }

        public void StopSkill()
        {
            skills[curSkill].StopSkill();
        }

        public void RandomSkillSelect()
        {
            BossPattern pattern = patterns[phase - 1];
            do
            {
                int randV = Random.Range(0, 100);

                if (pattern.weight[0] * 10 >= randV)
                    select = pattern.skillKinds[0];

                else if (pattern.weight[1] * 10 >= randV)
                    select = pattern.skillKinds[1];

                else if (pattern.weight[2] * 10 >= randV)
                    select = pattern.skillKinds[2];

                else
                    select = pattern.skillKinds[3];

                if (select == GolemSkill.ShockWave)
                {
                    if (DirectionFromPlayer().sqrMagnitude > 5.0f * 5.0f)
                        select = curSkill;
                }

            } while (curSkill == select);

            if(select == GolemSkill.RockThrow)
                anim.SetInteger("SkillID", 4);
            else if(select == GolemSkill.RockfallRain || select == GolemSkill.RockDrop)
                anim.SetInteger("SkillID", 3);
            else if (select == GolemSkill.RushAttack)
                anim.SetInteger("SkillID", 2);
            else
                anim.SetInteger("SkillID", 1);

            curSkill = select;
        }

        // 가장 가까운 플레이어 검색
        public void ClosePlayerResearch()
        {
            float distanceTemp = 1000.0f;
            for (int i = 0; i < playerTransforms.Length; i++)
            {
                float curDis = Vector3.SqrMagnitude(playerTransforms[i].position - trans.position);

                if (curDis < distanceTemp)
                {
                    closePlayerTrans = playerTransforms[i];
                    distanceTemp = curDis;
                }
            }
        }

        // 플레이어와의 거리 계산
        public Vector3 DirectionFromPlayer()
        {
            Vector3 dir = closePlayerTrans.position - trans.position;
            dir.y = 0;
            return dir;
        }

        // 플레이어 쫒기
        public void TracingClosePlayer()
        {
            Vector3 dir = DirectionFromPlayer();
            if (dir.sqrMagnitude > 3.4f * 3.4f)
            {
                Vector3 destination = closePlayerTrans.position;
                destination.y = trans.position.y;

                trans.position = Vector3.MoveTowards(trans.position, destination, moveSpeed * Time.deltaTime);

                if (dir != Vector3.zero)
                {
                    trans.rotation = Quaternion.RotateTowards(trans.rotation,
                    Quaternion.LookRotation(dir), 460 * Time.deltaTime);
                }
            }
        }

        // 공격이 판정됬을때
        public bool MeleeAttackCheak()
        {
            Vector3 dir = DirectionFromPlayer();
            if (dir.sqrMagnitude < 3.5f * 3.5f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                return true;
            }

            return false;
        }

        // 근접 공격
        public void MeleeAttack()
        {
            float dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
            Vector3 direction = closePlayerTrans.position - transform.position;
            if (direction.magnitude < distance)
            {
                if (Vector3.Dot(direction.normalized, transform.forward) > dotValue && attackFlag)
                {
                    attackFlag = false;
                    playerOnDamage.Invoke(CalDamage());
                }
            }
        }

        public int CalDamage()
        {
            return Random.Range(stats.MinDamage, stats.MaxDamage);
        }

        // 캐스팅 여부
        public bool CastingSuccess()
        {
            castingGauge += Time.deltaTime;
            if (castingGauge >= castingRisesTime)
            {
                return true;
            }

            return false;
        }

        // 캐스팅 리셋
        public void ResetCastingGage()
        {
            castingGauge = 0;
        }

        public AudioClip soundEffect;
        public void StartSound()
        {
            Core.SoundManager.OneShot(soundEffect);
        }
    }
}
