using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitStats : MonoBehaviour
    {
        public event Action<Stat> OnHpChanged;
        public event Action<Stat> OnStatChanged;

        private Stat hpStat;
        private Dictionary<StatType, Stat> stats;
        private Dictionary<StatType, List<StatModifier>> statModifiers
            = new Dictionary<StatType, List<StatModifier>>();

        public void TakeDamage(float damage)
        {
            hpStat.Value -= damage;
            OnHpChanged?.Invoke(hpStat);
        }

        public Stat? GetStat(StatType type)
        {
            if (stats.ContainsKey(type))
                return stats[type];
            else
                return null;
        }

        public void AddStatValue(StatType type, float value)
        {
            if (!stats.ContainsKey(type)) return;

            var stat = stats[type];
            stat.Value += value;

            stats[type] = stat;

            OnStatChanged?.Invoke(stat);
        }

        public void AddModifier(StatModifier modifier)
        {
            if (!stats.ContainsKey(modifier.StatType)) return;

            var stat = stats[modifier.StatType];
            modifier.Modify(ref stat);
            stats[modifier.StatType] = stat;

            OnStatChanged?.Invoke(stat);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (!stats.ContainsKey(modifier.StatType)) return;

            var stat = stats[modifier.StatType];
            modifier.DeModify(ref stat);
            stats[modifier.StatType] = stat;

            OnStatChanged?.Invoke(stat);

        }

        public void Init(Stat[] stats)
        {
            this.stats = new Dictionary<StatType, Stat>();

            foreach (var stat in stats)
            {
                stat.Init();

                if (stat.Type == StatType.Health)
                {
                    hpStat = stat;
                }

                this.stats.Add(stat.Type, stat);
            }
        }

        public void Reset()
        {
            foreach (var statEntry in stats)
            {
                if (statEntry.Value.Type == StatType.Health)
                {
                    OnHpChanged?.Invoke(hpStat);
                    continue;
                }

                statEntry.Value.Reset();
                OnStatChanged?.Invoke(statEntry.Value);
            }
        }
    }
}