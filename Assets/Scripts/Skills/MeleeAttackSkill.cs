using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Skills
{
    public class MeleeAttackSkill : Skill
    {
        private Stat damage;
        private Stat? vampirism;
        private EffectConfig vampirismEffectConfig;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            damage = (Stat)unit.Stats.GetStat(StatType.Damage);
            vampirism = unit.Stats.GetStat(StatType.Vampirism);
            vampirismEffectConfig = ManagerHolder.I.GetManager<EffectsConfig>().Vampirism;
        }

        public override void Execute()
        {
            target.TakeDamage(damage.Value);

            if (vampirism != null && ((Stat)vampirism).Value != 0)
            {
                var hp = damage.Value * ((Stat)vampirism).Value;
                unit.Stats.AddStatValue(StatType.Health, hp);

                Transform effectTargetTransform;

                if (unit.EffectPoint != null)
                {
                    effectTargetTransform = unit.EffectPoint;
                }
                else 
                {
                    effectTargetTransform = unit.transform;
                }

                var vampirismEffect = vampirismEffectConfig.Create();
                vampirismEffect.transform.parent = effectTargetTransform;
                vampirismEffect.transform.position = effectTargetTransform.position;
                vampirismEffect.StartEffect();
            }
        }
    }
}
