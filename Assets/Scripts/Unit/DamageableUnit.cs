using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class DamageableUnit : MonoBehaviour, IDamageable
    {
        public TargetType Type { get { return type; } }
        public Transform Transform
        {
            get
            { 
                if (viewTransform == null)
                {
                    return transform;
                }
                else
                {
                    return viewTransform;
                }
            }
        }

        public bool Alive => unit.Alive;
        
        [SerializeField]
        private UnitStats unitStats;
        [SerializeField]
        private Unit unit;
        [SerializeField]
        private TargetType type = TargetType.GroundUnit;
        [SerializeField]
        private Transform viewTransform;
        [SerializeField]
        private Vector3 viewOffset;

        private Stat? armor;
        private Stat? evasion;

        private float GetFinalDamage(float beginDamage)
        {
            armor = unitStats.GetStat(StatType.Armor);
            if (armor == null) return beginDamage;
            return beginDamage - beginDamage * ((Stat)armor).Value * 0.05f;
        }

        public void TakeDamage(float damage)
        {
            if (unit.Alive)
            {
                float finalDamage = GetFinalDamage(damage);
                evasion = unitStats.GetStat(StatType.Evasion);
                float chanceToEvade = 0;
                if (evasion != null)
                {
                    chanceToEvade = ((Stat)evasion).Value;
                }

                if (Random.Range(0.01f, 100) <= chanceToEvade)
                {
                    unit.Evade();
                    finalDamage = 0;
                }
                unitStats.TakeDamage(finalDamage);
            }
        }
    }
}