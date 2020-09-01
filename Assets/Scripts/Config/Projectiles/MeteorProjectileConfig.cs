using CastleFight.Core;
using CastleFight.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Meteor", menuName = "Projectiles/Meteor", order = 0)]
    public class MeteorProjectileConfig : ProjectileConfig
    {
        public override Projectile Create()
        {
            var projectile = Pool.I.Get<MeteorProjectile>();

            if (projectile == null)
            {
                projectile = Instantiate(prefab) as MeteorProjectile;
                projectile.Init(this);
            }
            else
            {
                projectile.gameObject.SetActive(true);
            }

            projectile.gameObject.SetActive(true);

            return projectile;
        }
    }
}