using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Skills
{
    public class MeleeAttackSkill : Skill
    {
        public override void Execute()
        {
            target.TakeDamage(unitConfig.Damage);
        }
    }
}
