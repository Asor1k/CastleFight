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

        public void TakeDamage(int damage)
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

        public void Init(Stat[] stats)
        {
            this.stats = new Dictionary<StatType, Stat>();

            foreach (var stat in stats)
            {
                stat.Init();

                if (stat.Type == StatType.Health)
                {
                    hpStat = stat;
                    continue;
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