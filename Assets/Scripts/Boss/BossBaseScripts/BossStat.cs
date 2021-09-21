using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossStat : MonoBehaviour
    {
        private int _str;
        private int _dex;
        private int _int;
        private int _level;

        public int STR
        {
            get { return _str; }
            set
            {
                _str = value;
                RefreshSTR();
            }
        }
        public int DEX
        {
            get { return _dex; }
            set
            {
                _dex = value;
                RefreshDEX();
            }
        }
        public int INT
        {
            get { return _int; }
            set
            {
                _int = value;
                RefreshINT();
            }
        }
        public int LEVEL
        {
            get { return _level; }
            set
            {
                _level = value;
                RefreshLEVEL();
            }
        }

        public float MoveSpeed;

        public readonly float MaxSkillGauge = 100.0f;

        private int hp;
        private int tpsGauge; //TimePerGauge
        private int asGauge; //Atk Skill Gauge
        private int minDamage;
        private int maxDamage;
        private int atkSpeed;
        private int armor;
        private float pnt; //Physical Near Tolerance

        public int HP { get { return hp; } }
        public int TPSGauge { get { return tpsGauge; } }
        public int ASGauge { get { return asGauge; } }
        public int MinDamage { get { return minDamage; } set { minDamage = value; } }
        public int MaxDamage { get { return maxDamage; } set { maxDamage = value; } }
        public int AttackSpeed { get { return atkSpeed; } }
        public int Armor { get { return armor; } }
        public float PNT { get { return pnt; } }

        private void Awake()
        {
            RefreshSTR();
            RefreshDEX();
            RefreshINT();
            RefreshLEVEL();
        }

        public void RefreshAllStats(int _str, int _dex, int _int, int _level, float moveSpeed)
        {
            this.STR = _str;
            this.DEX = _dex;
            this.INT = _int;
            this.LEVEL = _level;
            this.MoveSpeed = moveSpeed;
        }

        private void RefreshSTR()
        {
            // 체력
            this.hp = (4000 + (100 * _str) + (_level * 1000));

            // 1회 공격당 스킬 게이지
            this.asGauge = (10 + (int)(_str * 0.2f));

            // 최소 공격력
            this.minDamage = (int)(_str * 2.4f);

            // 최대 공격력
            this.maxDamage = (int)(_str * 2.8f);

            // 물리 근접 내성
            this.pnt = ((this.armor + _str) * 0.001f);
        }

        private void RefreshDEX()
        {
            // 공격 속도
            this.atkSpeed = 2 / (int)(1.4f + (0.02f * _dex));

            // 방어력
            this.armor = (1 + (int)(1.2f * _dex));
        }

        private void RefreshINT()
        {
            this.tpsGauge = (10 + (int)(0.2f * _int));
        }

        private void RefreshLEVEL()
        {
            this.hp = (4000 + (100 * _str) + (_level * 1000));
        }
    }
}