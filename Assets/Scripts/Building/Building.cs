using System;
using System.Collections;
using CastleFight.Config;
using CastleFight.Core;
using Core;
using UnityEngine;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using CastleFight.UI;
using UnityEngine.AI;
using BuildingStats = Castlefight.BuildingStats;

namespace CastleFight
{   
    [RequireComponent(typeof(BuildingStats))]
    public class Building : MonoBehaviour
    {
        public event Action OnReady;
        public event Action OnUpgrade;
        
        public BuildingBehavior Behavior => behavior;
        public BaseBuildingConfig Config => config;
        public GoldManager GoldManager => goldManager;
        public bool SpawnBlocked => spawnBlocked;
        public Transform SpawnPoint => spawnPoint;
        public int Lvl => lvl;
        public bool IsStanding => isStanding;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private BuildingBehavior behavior;
        [SerializeField] private BuildingStats stats;
        [SerializeField] private BuildingHealthBar healthBar;
        [SerializeField] private BuildingLevelLabel levelLabel;
        [SerializeField] private Collider col;
        [SerializeField] private NavMeshObstacle obstacle;
        
        private GoldManager goldManager;
        private BaseBuildingConfig config;
        private int lvl;
        private bool spawnBlocked = false;
        private bool isStanding = true;

        private void Start()
        {
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
        }

        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
            lvl = 1; //TODO: delete the magic number
            levelLabel.SetLevel(lvl);
            stats.Init(config.LevelsStats[0].MaxHp);
            stats.OnDamaged += OnDamage;
        }
        
        public void UpgradeBuilding()
        {
            if (lvl > config.LevelsStats.Count || !goldManager.IsEnough(behavior)) return;

            lvl++;  
            goldManager.MakeGoldChange(-config.LevelsStats[lvl - 1].Cost);
            stats.Init(config.LevelsStats[lvl-1].MaxHp);
            levelLabel.SetLevel(lvl);
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
            col.enabled = false;
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