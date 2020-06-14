using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class DamageableUnit : MonoBehaviour, IDamageable
    {
        public Transform Transform => transform;

        public bool Alive => unit.Alive;

        [SerializeField]
        private UnitStats unitStats;
        [SerializeField]
        private Unit unit;

        public void TakeDamage(int damage)
        {
            if(unit.Alive)
                unitStats.TakeDamage(damage);
        }
    }
}