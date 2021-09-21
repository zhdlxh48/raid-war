// 작성자: 임자현
// 작성일: 2019-09-06
// 설명: 보스 몬스터들의 베이스

using System.Collections;
using UnityEngine;

// 동기화 정보
// 보스의 좌표
// 사용 스킬
// 범위 공격 범위 동기화
// 발사체 동기화

namespace Boss
{
    public class BossMonsterBase : MonoBehaviour
    {
        public int startLevel;
        public int curHP;
        public int startSTR;
        public int startDEX;
        public int startINT;


        public Material bossBodyMat;
        public float moveSpeed;
        public BossStat stats;

        private bool isDead = false;
        private int curGrogiGauge = 0;
        public bool isAwake = true;

        public void OnDamage(int damage)
        {
            if (!isGame)
                return;

            StartCoroutine(MatColorSet());
            int da = (int)(damage * (1.0f - stats.PNT)) - stats.Armor;
            SprinkleDamageText.OnDamageText(DamageType.Nomal, HitActor.Enemy, this.transform.position, da);
            curGrogiGauge += da;
            curHP -= da;
        }

        WaitForSeconds waitMat = new WaitForSeconds(0.08f);
        Color c = new Color(1.0f, 0.7f, 0.7f);
        IEnumerator MatColorSet()
        {
            bossBodyMat.color = c;
            yield return waitMat;
            bossBodyMat.color = Color.white;
        }

        public bool OnDead()
        {
            if (curHP <= 0)
                return true;

            return false;
        }

        protected bool isGame = false;

        public void BossStart()
        {
            isGame = true;
        }

        public void BossEnd()
        {
            isGame = false;
        }

        public bool IsAwake()
        {
            if (curGrogiGauge >= 1000)
                isAwake = false;

            return isAwake;
        }

        public void Awaking()
        {
            curGrogiGauge = 0;
            isAwake = true;
        }
    }
}
