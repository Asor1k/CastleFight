using System.Collections;
using System.Collections.Generic;
using CastleFight.Projectiles;
using UnityEngine;

namespace CastleFight.Skills
{
    public class RangeAttackSkill : Skill
    {
        [SerializeField] private ProjectileConfig projectileConfig;
        [SerializeField] private Transform launchPoint;

        private Dictionary<Projectile, IDamageable> targetsCache = new Dictionary<Projectile, IDamageable>();
        private Stat damage;
        private Stat? vampirism;
        private Stat? crit;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            damage =(Stat) unit.Stats.GetStat(StatType.Damage);
            vampirism = unit.Stats.GetStat(StatType.Vampirism);
            crit = unit.Stats.GetStat(StatType.Crit);
        }

        public override void Execute()
        {
            var projectile = projectileConfig.Create();
            targetsCache.Add(projectile, target);
            projectile.Launch(launchPoint.position, target.Transform.gameObject, OnTargetReached);
        }

        private void OnTargetReached(Projectile projectile)
        {
            var cachedTarget = targetsCache[projectile];

            projectile.transform.parent = cachedTarget.Transform;

            if (cachedTarget == null)
                return;
            else
                targetsCache.Remove(projectile);

            float damage = this.damage.Value;
            
            if(crit != null)
            {
                if (Random.Range(0.01f, 100) <= ((Stat)crit).Value)
                {
                    damage *= 2;
                }
            }
            
            cachedTarget.TakeDamage(damage);

            if (vampirism != null)
            {
                var hp = damage * ((Stat)vampirism).Value;
                unit.Stats.AddStatValue(StatType.Health, hp);
            }

        }
    }
}