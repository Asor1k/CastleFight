using System;
using System.Collections.Generic;
using System.Collections;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public class BotController : MonoBehaviour
    {
        public List<Building> Buildings => buildings;

        [SerializeField] private List<BotStrategy> botStrategies;
        [SerializeField] private int strategyInd;
        //[SerializeField] private List<BotBuildPoint> buildSteps;
        [SerializeField] private CastlesPosProvider castlesPosProvider;
        [SerializeField] private float turnTime;
        [SerializeField] private float offsetY;
        [SerializeField] private int currBuildIndex;
        [SerializeField] private int maxBlocksBuilt;
        [SerializeField] private RaceSet raceSet;
        [SerializeField] private BotBehaviour botBehaviour;


        private List<BotBuildPoint> destroyedSteps = new List<BotBuildPoint>();
        private RaceConfig raceConfig;
        private int blocksBuilt = 0;
        private GoldManager goldManager;
        private BuildingsLimitManager buildingsLimitManager;
        private int destroyInd;
        private List<Building> buildings = new List<Building>();
        public void Init(RaceConfig config)
        {
            CreateCastle(config.CastleConfig);
            EventBusController.I.Bus.Subscribe<UnitDiedEvent>(OnUnitDie);
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            StartGame();
            buildingsLimitManager = ManagerHolder.I.GetManager<BuildingsLimitManager>();
            strategyInd = UnityEngine.Random.Range(0, botStrategies.Count);
            EventBusController.I.Bus.Subscribe<BuildingDestroyedEvent>(OnBuildingDestroyed);
        }
        private void OnUnitDie(UnitDiedEvent unitDiedEvent)
        {
            if (unitDiedEvent.Unit.gameObject.layer == (int)Team.Team1)
            {
                goldManager.MakeGoldChange(unitDiedEvent.Unit.Config.Cost, Team.Team2);
            }
            else
            {
                //DO something when your unit dies
            }
        }

        private void OnBuildingDestroyed(BuildingDestroyedEvent buildingDestroyedEvent)
        {
            if (buildingDestroyedEvent.building.gameObject.layer == (int)Team.Team1) return;
            botBehaviour.botAction = BotAction.BuildDestroyed;
            
            destroyedSteps.Add(new BotBuildPoint(buildingDestroyedEvent.building.transform, buildingDestroyedEvent.building.Config));
          
        }


        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y, castlePos.z); // TODO: remove magic number
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
            castle.gameObject.layer = (int)Team.Team2;
            castle.Init(castleConfig);
        }

        private void PlaceBuilding(int buildInd)
        {
            buildingsLimitManager.AddBuilding(Team.Team2);
            var building = botStrategies[strategyInd].buildSteps[buildInd].BuildingConfig.Create();
            buildings.Add(building);
            building.transform.position = botStrategies[strategyInd].buildSteps[buildInd].Point.position + new Vector3(0, building.Behavior.OffsetY,0);
            building.Behavior.Place(Team.Team2);
            SetRandomAction();
        }

        private void PlaceDestoyedBuilding(BotBuildPoint botBuildPoint)
        {
            buildingsLimitManager.AddBuilding(Team.Team2);
            var building = botBuildPoint.BuildingConfig.Create();
            buildings.Add(building);
            Debug.Log(botBuildPoint.Point.position);
            building.transform.position = new Vector3(botBuildPoint.Point.position.x, offsetY+ building.Behavior.OffsetY, botBuildPoint.Point.position.z);
            building.Behavior.Place(Team.Team2);
        }

        public void UpgradeBuilding(Building building)
        {
            building.UpgradeBuilding(Team.Team2);
            SetRandomAction();
        }
        private void SetRandomAction()
        {
            botBehaviour.botAction = (BotAction)UnityEngine.Random.Range(0, 2);
        }

        private IEnumerator BotGameTurn()
        {
            yield return new WaitForSeconds(turnTime);

            if (botBehaviour.botAction == BotAction.BuildDestroyed)
            {
                if (destroyedSteps.Count == 0)
                {
                    botBehaviour.botAction = BotAction.Upgrade;
                }
                else if (goldManager.BotGoldAmount >= destroyedSteps[0].BuildingConfig.Cost)
                {
                    PlaceDestoyedBuilding(destroyedSteps[0]);
                    destroyedSteps.Remove(destroyedSteps[0]);
                }
                StartCoroutine(BotGameTurn());
                yield break;
            }

            if (!botBehaviour.CanUpgrade())
            {
                if (currBuildIndex >= botStrategies[strategyInd].buildSteps.Count)
                {
                    StartCoroutine(BotGameTurn());
                    yield break;
                }
                if (goldManager.BotGoldAmount >= botStrategies[strategyInd].buildSteps[currBuildIndex].BuildingConfig.Cost && buildingsLimitManager.CanBuild(Team.Team2))
                {
                    PlaceBuilding(currBuildIndex);
                    goldManager.MakeGoldChange(-botStrategies[strategyInd].buildSteps[currBuildIndex].BuildingConfig.Cost, Team.Team2);
                    currBuildIndex++;
                }

                if (!buildingsLimitManager.CanBuild(Team.Team2))
                {
                    botBehaviour.botAction = BotAction.Upgrade;
                }
            }
            StartCoroutine(BotGameTurn());
        }

        public void StartGame()
        {
            currBuildIndex = 0;
            StartCoroutine(BotGameTurn());
        }

        public void StopGame()
        {

        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<UnitDiedEvent>(OnUnitDie);
            EventBusController.I.Bus.Unsubscribe<BuildingDestroyedEvent>(OnBuildingDestroyed);
        }
    }

/*   [Serializable]
    public enum BotState
    {
        Strategy,
        Upgradeble
    }
*/
    [Serializable]
    public class BotStrategy
    {
        public List<BotBuildPoint> buildSteps;
    }

    [Serializable]
    public class BotBuildPoint
    {
        public Transform Point;
        public BaseBuildingConfig BuildingConfig;
        public BotBuildPoint(Transform point, BaseBuildingConfig baseBuildingConfig)
        {
            Point = point;
            BuildingConfig = baseBuildingConfig;
        }
    }
}