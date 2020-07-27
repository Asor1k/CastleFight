using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [Serializable]
    public class StatModifier
    {
        public StatType StatType { get { return statType; } }
        [SerializeField]
        private int value;
        [SerializeField]
        private StatType statType;
        [SerializeField]
        private ModifierType Type;

        public void Modify(ref Stat stat)
        {
            if (Type == ModifierType.Increase)
            {
                stat.Value += this.value;
            }
            else if (Type == ModifierType.Multiply)
            {
                stat.Value *= this.value;
            }
        }

        public void DeModify(ref Stat stat)
        {
            if (Type == ModifierType.Increase)
            {
                stat.Value -= this.value;
            }
            else if (Type == ModifierType.Multiply)
            {
                stat.Value /= this.value;
            }
        }
    }

    public enum ModifierType
    {
        Increase,
        Multiply
    }
}