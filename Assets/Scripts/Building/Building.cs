using System;
using System.Collections;
using Castlefight;
using Core;
using UnityEngine;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine.AI;

namespace CastleFight
{
    public class Building : MonoBehaviour
    {
        public event Action OnReady;
        public BuildingBehavior Behavior => behavior;
        public BaseBuildingConfig Config => config;
        public bool SpawnBlocked => spawnBlocked;
        public Transform SpawnPoint => spawnPoint;
        public int Lvl => lvl;
        public bool IsStanding => isStanding;

        [SerializeField]
        private Transform spawnPoint;
        [SerializeField] private BuildingBehavior behavior;
        [SerializeField] private BuildingStats stats;
        [SerializeField] private BuildingHealthBar healthBar;
        [SerializeField] private Collider collider;
        [SerializeField] private NavMeshObstacle obstacle;
        
        private BaseBuildingConfig config;
        private int lvl;
        private bool spawnBlocked = false;
        private bool isStanding = true;
        
        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
            lvl = 1; //TODO: delete the magic number
            stats.Init(config.MaxHp);
            stats.OnDamaged += OnDamage;
        }
        

        
        public void UpgradeBuilding()
        {

            if (behavior.goldManager.IsEnough(behavior))
            {
                behavior.goldManager.MakeGoldChange(-config.Cost);
                lvl++; //Upgrade
            }
        }

        public void Build()
        {
            healthBar.Show(true);
            //TODO: Implement building construction
            OnReady?.Invoke();
        }

        public void Destroy()
        {
            isStanding = false;
            collider.enabled = false;
            obstacle.enabled = false;
            StartCoroutine(DestroyCoroutine());
        }

        private void OnDamage()
        {
            if (stats.Hp <= 0)
            {
                Destroy();
            }
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