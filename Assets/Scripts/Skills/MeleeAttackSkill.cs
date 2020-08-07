using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Skills
{
    public class MeleeAttackSkill : Skill
    {
        private Stat damage;
        private Stat? vampirism;
        public override void Init(Unit unit)
        {
            base.Init(unit);
            damage = (Stat)unit.Stats.GetStat(StatType.Damage);
            vampirism = (Stat)unit.Stats.GetStat(StatType.Vampirism);
        }

        public override void Execute()
        {
            target.TakeDamage(damage.Value);
            if (vampirism != null)
            {
                var hp = damage.Value * ((Stat)vampirism).Value;
                unit.Stats.AddStatValue(StatType.Health, hp);
            }
        }
    }
}
