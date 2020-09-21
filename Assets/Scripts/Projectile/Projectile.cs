using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public Team OwnerTeam => ownerTeam;

        [SerializeField] protected float speed;
        [SerializeField] protected float hitDistance;
        [SerializeField] protected float destroyDelay;
        [SerializeField] protected List<ParticleSystem> runEffects;
        [SerializeField] protected List<ParticleSystem> hitEffects;
        [SerializeField] protected bool lookAtTarget;

        protected Action<Projectile> targetReachCallback;
      
        protected GameObject target;
        protected Vector3 targetPoint;
        protected bool isMoving = false;
        protected Team ownerTeam;

        public void Init(ProjectileConfig config)
        {
            speed = config.Speed;
            hitDistance = config.HitDistance;
            destroyDelay = config.DestroyDelay;
        }

        public void Launch(Vector3 point, Team ownerTeam, GameObject target, Action<Projectile> targetReachCallback)
        {
            this.ownerTeam = ownerTeam;
            this.target = target;
            this.targetReachCallback = targetReachCallback;
            transform.position = point;
            isMoving = true;
            
            RunStartParticles();
        }

        public void Launch(Vector3 point, Team ownerTeam, Vector3 targetPoint, Action<Projectile> targetReachCallback)
        {
            this.ownerTeam = ownerTeam;
            this.targetPoint = targetPoint;
            this.targetReachCallback = targetReachCallback;
            target = null;
            transform.position = point;
            isMoving = true;

            RunStartParticles();
        }

        protected void RunStartParticles()
        {
            ToggleEffects(hitEffects, false);
            ToggleEffects(runEffects, true);
        }

        protected void RunHitParticles()
        {
            ToggleEffects(runEffects, false);    
            ToggleEffects(hitEffects, true);
        }
        

        protected virtual void OnHit()
        {
            if (target != null)
            {
                transform.parent = target.transform;
            }

            ToggleEffects(runEffects, false);
        }

        private void ToggleEffects(List<ParticleSystem> effects, bool run)
        {
            foreach (var effect in effects)
            {
                if (run)
                {
                    RunEffect(effect);
                }
                else
                {
                    StopEffect(effect);
                }
            }
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

        protected virtual void Move()
        {
            var projectilePosition = transform.position;
            Vector3 targetPosition;

            if (target != null)
            {
                targetPosition = target.transform.position;
            }
            else
            {
                targetPosition = targetPoint;
            }


            if(lookAtTarget) transform.LookAt(targetPosition);

            transform.position = Vector3.MoveTowards(projectilePosition, targetPosition, speed * Time.deltaTime);
        }

        public void Update()
        {
            if (!isMoving) return;
            transform.parent = null;

            Vector3 targetPosition;

            if (target != null)
            {
                targetPosition = target.transform.position;
            }
            else
            {
                targetPosition = targetPoint;
            }

            var projectilePosition = transform.position;
            var distance = Vector3.Distance(projectilePosition, targetPosition);

            if (distance <= hitDistance)
            {
                isMoving = false;
                RunHitParticles();
                OnHit();
                targetReachCallback?.Invoke(this);
            }

            Move();
        }
    }
}