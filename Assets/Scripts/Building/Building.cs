using System;
using System.Collections;
using CastleFight.Config;
using CastleFight.Core;
using Core;
using UnityEngine;
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
        [SerializeField] private BuildingUpgradeButton upgradeButton;
        [SerializeField] private DestroyBuildingUI destroyButton;
        [SerializeField] private BuildingLevelLabel levelLabel;
        [SerializeField] private Collider col;
        [SerializeField] private NavMeshObstacle obstacle;
        
        private GoldManager goldManager;
        private BaseBuildingConfig config;
        private int lvl;
        private bool spawnBlocked = false;
        private bool isStanding = true;
        private BuildingsLimitManager buildingsLimitManager;
        private void Start()
        {
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            buildingsLimitManager = ManagerHolder.I.GetManager<BuildingsLimitManager>();
        }

        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
            lvl = 1; //TODO: delete the magic number
            levelLabel.SetLevel(lvl);
            stats.Init(config.Levels[0].MaxHp);
            stats.OnDamaged += OnDamage;
            UpdateUpgradeLabel();
        }
        
        public void SellBuilding()
        {
            Destroy();
            goldManager.MakeGoldChange(config.Levels[lvl - 1].SumForSale, Team.Team1);
        }

        public void UpgradeBuilding()
        {
            if (lvl >= config.Levels.Count || !goldManager.IsEnough(config.Levels[lvl].Cost)) return;

            lvl++;  
            goldManager.MakeGoldChange(-config.Levels[lvl - 1].Cost,Team.Team1);
            stats.Init(config.Levels[lvl-1].MaxHp);
            levelLabel.SetLevel(lvl);
            UpdateUpgradeLabel();
        }

        public void Build()
        {
            healthBar.Show(true);
            //TODO: Implement building construction
            OnReady?.Invoke();
        }

        public void Select()
        {
            destroyButton.Show();
            if (lvl == config.Levels.Count) return;
            upgradeButton.Show();
        }

        public void Deselect()
        {
            upgradeButton.Hide();
            destroyButton.Hide();
        }

        private void UpdateUpgradeLabel()
        {
            if(lvl < config.Levels.Count)
               upgradeButton.SetCostLabel(config.Levels[lvl].Cost.ToString());
             else
                upgradeButton.Hide();
        }

        public void Destroy()
        {
            if (!isStanding) return;
            isStanding = false;
            col.enabled = false;
            obstacle.enabled = false;
            buildingsLimitManager.DeleteBuilding((Team)gameObject.layer);
            Deselect();
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
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}