using System;
using System.Collections;
using CastleFight.Config;
using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using CastleFight.UI;
using UnityEngine.AI;
using BuildingStats = Castlefight.BuildingStats;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace CastleFight
{   
    [RequireComponent(typeof(BuildingStats))]
    public class Building : MonoBehaviour
    {
        public event Action OnReady;
        public event Action OnUpgrade;
        
        public BuildingBehavior Behavior => behavior;
        public BaseBuildingConfig Config => config;
        public BuildingUpgradeNode CurrentLevelConfig => currentLevelConfig;
        public GoldManager GoldManager => goldManager;
        public bool SpawnBlocked => spawnBlocked;
        public Transform SpawnPoint => spawnPoint;
        public int Lvl => lvl;
        public bool IsStanding => isStanding;
        public IDamageable Damageable => damageable;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private BuildingBehavior behavior;
        [SerializeField] private BuildingStats stats;
        [SerializeField] private BuildingHealthBar healthBar;
        [SerializeField] private List<BuildingUpgradeButton> upgradeButtons;
        [SerializeField] private DestroyBuildingUI destroyButton;
        [SerializeField] private BuildingLevelLabel levelLabel;
        [SerializeField] private Collider col;
        [SerializeField] private NavMeshObstacle obstacle;

        private IDamageable damageable;
        private GoldManager goldManager;
        private BaseBuildingConfig config;
        private BuildingUpgradeNode currentLevelConfig;
        private int lvl;
        private bool spawnBlocked = false;
        private bool isStanding = true;
        private BuildingsLimitManager buildingsLimitManager;
        private AudioManager audioManager;
        private bool selected;

        private void Awake()
        {
            damageable = GetComponent<IDamageable>();
            upgradeButtons = GetComponentsInChildren<BuildingUpgradeButton>(true).ToList();

            foreach (var upgradeButton in upgradeButtons)
            {
                upgradeButton.SetBuilding(this);
            }

            destroyButton.SetBuilding(this);
        }

        private void Start()
        {
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            buildingsLimitManager = ManagerHolder.I.GetManager<BuildingsLimitManager>();
            audioManager = ManagerHolder.I.GetManager<AudioManager>();
        }

        public void Init(BaseBuildingConfig config)
        {
            this.config = config;
            currentLevelConfig = config.LevelUgradeTree;
            lvl = 1; //TODO: delete the magic number
            levelLabel.SetLevel(lvl, config.GetMaxLevels());

            stats.Init(currentLevelConfig.Config.MaxHp);
            stats.OnDamaged += OnDamage;
            UpdateUpgradeLabel();
        }
        
        public void SellBuilding()
        {
            Destroy();
            goldManager.InitGoldAnim(currentLevelConfig.Config.SumForSale, transform);
            goldManager.MakeGoldChange(currentLevelConfig.Config.SumForSale, Team.Team1);
            audioManager.Play("Sell building");
        }

        public void UpgradeBuilding(int nodeIndex, Team team = Team.Team1)
        {
            if(team==Team.Team1)
                if (currentLevelConfig.Nodes.Count == 0 || !goldManager.IsEnough(currentLevelConfig.Nodes[nodeIndex].Config.Cost)) return;

            audioManager.Play("Building Upgrade");
            lvl++;
            currentLevelConfig = currentLevelConfig.Nodes[nodeIndex];
            goldManager.MakeGoldChange(-currentLevelConfig.Config.Cost, team);
            stats.Init(currentLevelConfig.Config.MaxHp);
            EventBusController.I.Bus.Publish(new BuildingUpgradedEvent(this));
            levelLabel.SetLevel(lvl, config.GetMaxLevels());
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
            selected = true;
            destroyButton.Show();
            ShowUpgradeButtons(true);
        }

        public void Deselect()
        {
            selected = false;
            ShowUpgradeButtons(false);
            destroyButton.Hide();
        }

        private void UpdateUpgradeLabel()
        {
            if (currentLevelConfig.Nodes.Count != 0)
            {
                for(int i = 0; i < upgradeButtons.Count;i++)
                {
                    if (i < currentLevelConfig.Nodes.Count)
                    {
                        Debug.Log("---" + currentLevelConfig.Nodes[i].Config.Cost);
                        upgradeButtons[i].SetLabels(currentLevelConfig.Nodes[i].Config.Icon, currentLevelConfig.Nodes[i].Config.Cost, currentLevelConfig.Nodes[i].Config.Level);
                        upgradeButtons[i].SetInited(true);
                    }
                    else
                    {
                        upgradeButtons[i].SetInited(false);
                    }
                }

                if (selected)
                {
                    ShowUpgradeButtons(true);
                }
            }
            else
            {
                ShowUpgradeButtons(false);
            }
        }

        private void ShowUpgradeButtons(bool show)
        {
            foreach (var upgradeButton in upgradeButtons)
            {
                if (show)
                {
                    if (upgradeButton.IsInited)
                        upgradeButton.Show();
                    else
                        upgradeButton.Hide();
                }
                else
                {
                    upgradeButton.Hide();
                }
            }
        }

        public void Destroy()
        {
            if (!isStanding) return;
            isStanding = false;
            col.enabled = false;
            obstacle.enabled = false;
            buildingsLimitManager.DeleteBuilding((Team)gameObject.layer);
            EventBusController.I.Bus.Publish(new BuildingDestroyedEvent(this));
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