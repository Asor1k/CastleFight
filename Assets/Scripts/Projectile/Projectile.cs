using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] protected float hitDistance;
        [SerializeField] protected float destroyDelay;
        [SerializeField] protected ParticleSystem runEffect;
        [SerializeField] protected ParticleSystem hitEffect;
        
        protected Action<Projectile> targetReachCallback;
        
        private GameObject target;
        private bool isMoving = false;
        
        public void Init(ProjectileConfig config)
        {
            speed = config.Speed;
            hitDistance = config.HitDistance;
            destroyDelay = config.DestroyDelay;
        }

        public void Launch(Vector3 point, GameObject target, Action<Projectile> targetReachCallback)
        {
            this.target = target;
            this.targetReachCallback = targetReachCallback;
            transform.position = point;
            isMoving = true;
            
            RunStartParticles();
        }

        protected void RunStartParticles()
        {
            StopEffect(hitEffect);
            RunEffect(runEffect);
        }

        protected void RunHitParticles()
        {
            StopEffect(runEffect);    
            RunEffect(hitEffect);
        }
        

        protected virtual void OnHit()
        {
            RunEffect(hitEffect);
            StopEffect(runEffect);
        }

        private void RunEffect(ParticleSystem effect)
        {
            if (effect != null)
            {
                effect.gameObject.SetActive(true);
                effect.time = 0;
                effect.Play();
            }
        }

        private void StopEffect(ParticleSystem effect)
        {
            if (effect != null)
            {
                effect.gameObject.SetActive(false);
                effect.Stop();
            }
        }

        public void Update()
        {
            if (!isMoving) return;
            
            var targetPosition = target.transform.position;
            var projectilePosition = transform.position;
            var distance = Vector3.Distance(projectilePosition, targetPosition);

            if (distance <= hitDistance)
            {
                isMoving = false;
                RunHitParticles();
                OnHit();
                targetReachCallback?.Invoke(this);
            }
            
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(projectilePosition, targetPosition, speed * Time.deltaTime);
        }
    }
}