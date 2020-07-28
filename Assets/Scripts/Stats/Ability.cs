using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [Serializable]
    public class Ability
    {
        public bool IsActive { get { return isActive; } }
        public StatModifier[] Modifiers { get { return modifiers.ToArray(); } }

        [SerializeField]
        private List<StatModifier> modifiers;
        [SerializeField]
        private bool isActive;

        public void ChangeStatus(bool status)
        {
            isActive = status;
        }

        public virtual void Activate(Unit casterUnit)
        {
            if (modifiers.Count > 0)
            {
                foreach (var modifier in modifiers)
                {
                    casterUnit.Stats.AddModifier(modifier);
                }
            }
        }

        public virtual void Deactivate(Unit casterUnit)
        {
            if (modifiers.Count > 0)
            {
                foreach (var modifier in modifiers)
                {
                    casterUnit.Stats.RemoveModifier(modifier);
                }
            }
        }
    }
}