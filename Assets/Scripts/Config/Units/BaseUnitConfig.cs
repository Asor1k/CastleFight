using System;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    public enum UnitKind
    {
        Skeleton,
        Ghost,
        Death,
        Knight,
        Gryphon,
        Angel
    }

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

        public List<Ability> Abilities
        {
            get { return abilities; }
        }

        public UnitKind UnitKind
        {
            get { return unitKind; }
        }

        public Sprite Icon => icon;

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
        [SerializeField]
        protected List<Ability> abilities;
        [SerializeField]
        protected UnitKind unitKind;
        public abstract Unit Create(Team team);
 
      /*  [ContextMenu("Add To Generator")]
        public void AddToGenerator()
        {
            TalantsGenerator.I.AddConfig(this);
        }*/

        [ContextMenu("Fill stats")]
        public void FillDefaultStats() 
        {
            stats = new List<Stat>();
            stats.Add(new Stat(500, StatType.Health));
            stats.Add(new Stat(500, StatType.MaxHealth));
            stats.Add(new Stat(3, StatType.Speed));
            stats.Add(new Stat(100, StatType.Damage));
            stats.Add(new Stat(1, StatType.AttackDelay));
            stats.Add(new Stat(10, StatType.EnemyDetectRange));
            stats.Add(new Stat(3, StatType.AttackRange));
            stats.Add(new Stat(0, StatType.Vampirism));
            stats.Add(new Stat(0, StatType.Armor));
            stats.Add(new Stat(0, StatType.Evasion));
        }
    }
}