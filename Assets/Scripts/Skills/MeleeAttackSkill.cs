using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Skills
{
    public class MeleeAttackSkill : Skill
    {
        private Stat damage;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            damage = (Stat)unit.Stats.GetStat(StatType.Damage);
        }

        public override void Execute()
        {
            target.TakeDamage(damage.Value);
        }
    }
}
