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

        public void TakeDamage(float damage)
        {
            if(unit.Alive)
                unitStats.TakeDamage(damage);
        }
    }
}