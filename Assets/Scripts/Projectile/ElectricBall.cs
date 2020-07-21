using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Projectiles
{
    public class ElectricBall : Projectile
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
            Pool.I.Put<ElectricBall>(this);
        }
    }
}
