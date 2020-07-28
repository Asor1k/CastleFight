using System.Collections;
using System.Collections.Generic;
using CastleFight;
using CastleFight.Core;
using CastleFight.Projectiles;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Electric Ball", menuName = "Projectiles/Electric Ball", order = 0)]
    public class ElectricBallConfig : ProjectileConfig
    {
        
        public override Projectile Create()
        {
            var projectile = Pool.I.Get<ElectricBall>();

            if (projectile == null)
            {
                projectile = Instantiate(prefab) as ElectricBall;
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