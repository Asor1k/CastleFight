using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using System;
using System.Collections;
using Castlefight;
using UnityEngine.AI;

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
        
        private CastleConfig config;
        private bool isStanding = true;
        
        public void Init(CastleConfig config)
        {
            this.config = config;
            stats.Init(config.MaxHp);
            if((Team)gameObject.layer == Team.Team2)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
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
                EventBusController.I.Bus.Publish(new GameEndEvent((Team)gameObject.layer));
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
        
    }
}