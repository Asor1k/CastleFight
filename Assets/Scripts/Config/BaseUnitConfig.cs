using System;
using UnityEngine;

namespace CastleFight.Config
{
    public abstract class BaseUnitConfig : ScriptableObject
    {
        public float Speed{get{return speed;}}
        public float EnemyDetectRange{get{return enemyDetectRange;}}
        public float AttackDistance{get{return attackDistance;}}

        [SerializeField]
        protected float speed;
        [SerializeField]
        protected float attackDistance;
        [SerializeField]
        protected Unit prefab;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected float enemyDetectRange;
        public abstract Unit Create(Team team);
    }
}