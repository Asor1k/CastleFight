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

        public int Cost
        {
            get { return cost; }
        }

        public float AttackDelay
        {
            get => attackDelay;
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
        [SerializeField]
        protected float attackDelay;
        [SerializeField]
        protected int damage;
        [SerializeField]
        protected int cost;

        public abstract Unit Create(Team team);
    }
}