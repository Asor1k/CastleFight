using System.Collections;
using System.Collections.Generic;
using CastleFight;
using CastleFight.Core;
using CastleFight.Projectiles;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Fire Ball", menuName = "Projectiles/Fire Ball", order = 0)]
    public class FireBallConfig : ProjectileConfig
    {

        public override Projectile Create()
        {
            var projectile = Pool.I.Get<FireBall>();

            if (projectile == null)
            {
                projectile = Instantiate(prefab) as FireBall;
                projectile.Init(this);
            }
            else
            {
                projectile.gameObject.SetActive(true);
            }

            return projectile;
        }
    }
}