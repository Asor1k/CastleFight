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
        
        public override void Execute()
        {
            var projectile = projectileConfig.Create();
            targetsCache.Add(projectile, target);
            projectile.Launch( launchPoint.position, target.Transform.gameObject, OnTargetReached);
        }

        private void OnTargetReached(Projectile projectile)
        {
            var cachedTarget = targetsCache[projectile];

            if (cachedTarget == null)
                return;
            else
                targetsCache.Remove(projectile);

            cachedTarget.TakeDamage(unitConfig.Damage);
        }
    }
}