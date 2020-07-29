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
        public float Value;
        private float startValue;
        private List<StatModifier> modifiers;

        public void Init()
        {
            startValue = Value;
        }

        public Stat(float value, StatType type)
        {
            this.Value = value;
            startValue = value;
            Type = type;
            modifiers = new List<StatModifier>();
        }

        public void Reset() 
        {
            Value = startValue;
        }
    }

    public enum StatType
    {
        Health,
        MaxHealth,
        Mana,
        Armor,
        Damage,
        Speed,
        AttackSpeed,
        AttackDelay,
        EnemyDetectRange,
        AttackRange,
        Vampirism
    }
}
