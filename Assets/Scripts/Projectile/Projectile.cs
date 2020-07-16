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
        [SerializeField] protected ParticleSystem startEffect;
        [SerializeField] protected ParticleSystem runEffect;
        [SerializeField] protected ParticleSystem hitEffect;
        
        private GameObject target;
        private bool isMoving;
        
        
        public void Init(GameObject target)
        {
            target = target;
        }

        public void Launch()
        {
            RunStartParticles();
            OnStart();
        }

        protected void RunStartParticles()
        {
            startEffect.Play();
            runEffect.Play();
        }

        protected void RunHitParticles()
        {
            runEffect.Stop();
            hitEffect.Play();
        }

        protected abstract void OnStart();
        protected abstract void OnHit();

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
            }
            
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(projectilePosition, targetPosition, speed * Time.deltaTime);
        }
    }
}