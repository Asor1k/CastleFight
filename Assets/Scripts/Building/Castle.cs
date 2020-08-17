using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using System;
using System.Collections;
using Castlefight;
using UnityEngine.AI;
using CastleFight.Skills;
using System.Threading.Tasks;
using System.Collections.Generic;
using CastleFight.Projectiles;

namespace CastleFight
{
    public class Castle : MonoBehaviour, IDamageable
    {
        public event Action OnReady;
        public CastleConfig Config => config;

        public bool Alive
        {
            get { return isStanding; }
        }


        public TargetType Type => TargetType.Castle;

        public Transform Transform => transform;

        public Vector3 ViewOffset => Vector3.zero;

        [SerializeField] private BuildingStats stats;
        [SerializeField] private Collider collider;
        [SerializeField] private NavMeshObstacle obstacle;
        [SerializeField] private ProjectileConfig projectileConfig;
        [SerializeField] private Transform launchPoint;

        private TargetProvider targetProvider = new TargetProvider();
        private CastleConfig config;
        private bool isStanding = true;
        private IDamageable currentTarget;
        private Coroutine updateTargetCoroutine;
        private float targetUpdateDelay = 0.1f;
        private LayerMask enemyLayer;
        private bool readyToAttack = true;
        private Dictionary<Projectile, IDamageable> targetsCache = new Dictionary<Projectile, IDamageable>();

        public void Init(CastleConfig config)
        {
            this.config = config;
            stats.Init(config.MaxHp);
            if ((Team)gameObject.layer == Team.Team2)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (gameObject.layer == (int)Team.Team1)
            {
                enemyLayer = LayerMask.GetMask("Team2");
            }
            else
            {
                enemyLayer = LayerMask.GetMask("Team1");
            }
        }

        void Update()
        {
            if (!isStanding)
            {
                if (updateTargetCoroutine != null)
                {
                    StopCoroutine(updateTargetCoroutine);
                    updateTargetCoroutine = null;
                }

                return;
            }

            if (currentTarget != null)
            {
                if (updateTargetCoroutine != null)
                {
                    StopCoroutine(updateTargetCoroutine);
                    updateTargetCoroutine = null;
                }

                if (!currentTarget.Alive)
                {
                    currentTarget = null;
                    return;
                }

                var distanceToTarget = Vector3.Distance(transform.position, currentTarget.Transform.position);

                if (distanceToTarget <= config.AttackRange && readyToAttack)
                {
                    Attack(currentTarget);
                    readyToAttack = false;
                    StartAttackCooldown();
                }
                else if (distanceToTarget > config.AttackRange)
                {
                    currentTarget = null;
                }
            }
            else
            {
                if (updateTargetCoroutine == null)
                {
                    updateTargetCoroutine = StartCoroutine(UpdateTargetCoroutine());
                }
            }
        }

        private IEnumerator UpdateTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(targetUpdateDelay);
                var target = targetProvider.GetTarget(enemyLayer, transform.position, config.AttackRange, false);

                if (target != null)
                {
                    currentTarget = target;
                }
            }
        }

        private void Attack(IDamageable target)
        {
            var projectile = projectileConfig.Create();
            targetsCache.Add(projectile, target);
            projectile.Launch(launchPoint.position, target.Transform.gameObject, (Projectile hitProjectile) =>
            {
                var cachedTarget = targetsCache[hitProjectile];

                hitProjectile.transform.parent = cachedTarget.Transform;

                if (cachedTarget == null)
                    return;
                else
                    targetsCache.Remove(hitProjectile);

                float damage = config.Damage;

                cachedTarget.TakeDamage(damage);

            });
        }

        public void Build()
        {
            //TODO: Implement building construction
            OnReady?.Invoke();
        }
        
        //TODO: refactor remove IDamageable from this class
        public void TakeDamage(float damage)
        {
            if (!isStanding) return;
            stats.TakeDamage(damage);

            if (stats.Hp <= 0)
            {
                isStanding = false;
                EventBusController.I.Bus.Publish(new GameEndEvent((Team)gameObject.layer,config.Race));
                Destroy();
            }
        }
        
        public void Destroy()
        {
            isStanding = false;
            collider.enabled = false;
            obstacle.enabled = false;
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            float timer = 3; //TODO: delete the magic number

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                transform.Translate(0,-1 * Time.deltaTime,0);//TODO: delete the magic number
                yield return null;
            }
        }

        private async Task StartAttackCooldown()
        {
            var miliseconds = config.AttackDelay * 1000;
            await Task.Delay((int)miliseconds);

            readyToAttack = true;
        }

    }
}