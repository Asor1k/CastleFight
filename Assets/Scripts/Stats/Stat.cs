using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [Serializable]
    public struct Stat
    {
        public StatType Type;
        public int Value;
        public int? MaxValue { get { if (maxValue == null) return null; else return maxValue; } }
        private int startValue;
        [SerializeField]
        private int maxValue;

        public void Init()
        {
            startValue = Value;
        }

        public Stat(int value, int maxValue, StatType type)
        {
            this.Value = value;
            this.maxValue = maxValue;
            startValue = value;
            Type = type;
        }

        public void Reset() 
        {
            if (MaxValue == null)
            {
                Value = startValue;
            }
            else
            {
                Value = (int)MaxValue;
            }
        }
    }

    public enum StatType
    {
        Health,
        Mana,
        Armor,
        Damage,
        Speed,
        AttackDelay,
        EnemyDetectRange,
        AttackRange
    }
}
