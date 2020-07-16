using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Projectiles
{
    public class ElectricBall : Projectile
    {
        protected override void OnStart()
        {
            
        }

        protected override void OnHit()
        {
            gameObject.SetActive(false);
            Pool.I.Put<ElectricBall>(this);
        }
    }
}
