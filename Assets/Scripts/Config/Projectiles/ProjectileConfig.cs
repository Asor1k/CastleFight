using System.Collections;
using System.Collections.Generic;
using CastleFight.Projectiles;
using UnityEngine;

namespace CastleFight
{
    public abstract class ProjectileConfig : ScriptableObject
    {
        public float Speed => speed;
        public float HitDistance => hitDistance;
        public float DestroyDelay => destroyDelay;
        
        [SerializeField] protected Projectile prefab;
        [SerializeField] protected float speed;
        [SerializeField] protected float hitDistance;
        [SerializeField] protected float destroyDelay;

        public abstract Projectile Create();
    }
}