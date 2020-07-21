using System;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu]
    public abstract class BaseUnitConfig : ScriptableObject
    {
        public int Cost
        {
            get { return cost; }
        }
        public float goldDmgFraction
        {
            get { return goldDmgPercent / 100f; }
        }

        public List<Stat> Stats
        { 
            get
            { 
                return stats;
            }
        }

        [SerializeField]
        protected List<Stat> stats;
        [SerializeField]
        protected Unit prefab;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected int cost;
        [SerializeField]
        protected float goldDmgPercent;


        public abstract Unit Create(Team team);

        [ContextMenu("Fill stats")]
        public void FillDefaultStats() 
        {
            stats = new List<Stat>();
            stats.Add(new Stat(500,500, StatType.Health));
            stats.Add(new Stat(3, 0, StatType.Speed));
            stats.Add(new Stat(100, 0, StatType.Damage));
            stats.Add(new Stat(1, 0, StatType.AttackDelay));
            stats.Add(new Stat(10, 0, StatType.EnemyDetectRange));
            stats.Add(new Stat(3, 0, StatType.AttackRange));
        }
    }
}