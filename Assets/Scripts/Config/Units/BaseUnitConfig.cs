using System;
using UnityEngine;

namespace CastleFight.Config
{
    public abstract class BaseUnitConfig : ScriptableObject
    {
        public int MaxHp
        {
            get { return maxHp; }
        }

        public float Speed
        {
            get { return speed; }
        }

        public float EnemyDetectRange
        {
            get { return enemyDetectRange; }
        }

        public float AttackDistance
        {
            get { return attackDistance; }
        }

        public int Damage
        {
            get { return damage; }
        }

    [SerializeField]
        protected float speed;
        [SerializeField]
        protected float attackDistance;
        [SerializeField]
        protected int maxHp;
        [SerializeField]
        protected Unit prefab;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected float enemyDetectRange;

        [SerializeField] protected int damage;
        
        public abstract Unit Create(Team team);
    }
}