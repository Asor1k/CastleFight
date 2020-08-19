using CastleFight.Core;
using CastleFight.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Projectiles
{
    public class MeteorProjectile : Projectile
    {
        protected override void OnHit()
        {
            base.OnHit();
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            yield return new WaitForSeconds(destroyDelay);
            transform.parent = null;
            gameObject.SetActive(false);
            Pool.I.Put<MeteorProjectile>(this);
        }
    }
}