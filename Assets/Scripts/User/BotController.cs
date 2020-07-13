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
        [SerializeField] private List<BotBuildStep> buildSteps;
        [SerializeField] private CastlesPosProvider castlesPosProvider;
        [SerializeField] private float turnTime;
        [SerializeField] private Vector3 stepOffset;
        [SerializeField] private int currBuildIndex;
        [SerializeField] private int maxBlocksBuilt;

        private int blocksBuilt = 0;
        private GoldManager goldManager;
        private BuildingsLimitManager buildingsLimitManager;

        public void Init(RaceConfig config)
        {
            CreateCastle(config.CastleConfig);
            EventBusController.I.Bus.Subscribe<UnitDiedEvent>(OnUnitDie);
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            StartGame();
            buildingsLimitManager = ManagerHolder.I.GetManager<BuildingsLimitManager>();
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
            var building = buildSteps[buildInd].BuildingConfig.Create();
            building.transform.position = buildSteps[buildInd].Point.position + stepOffset*blocksBuilt + new Vector3(0,building.Behavior.OffsetY,0);
            building.GetComponent<BuildingBehavior>().Place(Team.Team2);
        }

        private IEnumerator BotGameTurn()
        {
            yield return new WaitForSeconds(turnTime);
            if (currBuildIndex >= buildSteps.Count)
            {
                currBuildIndex = 0;
                if (blocksBuilt>=maxBlocksBuilt) yield break;
                blocksBuilt++;
            }
            if (goldManager.BotGoldAmount >= buildSteps[currBuildIndex].BuildingConfig.Cost && buildingsLimitManager.CanBuild(Team.Team2))
            {
                PlaceBuilding(currBuildIndex);
                goldManager.MakeGoldChange(-buildSteps[currBuildIndex].BuildingConfig.Cost, Team.Team2);
                currBuildIndex++;
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
        }
    }

    [Serializable]
    public class BotBuildStep
    {
        public Transform Point;
        public BaseBuildingConfig BuildingConfig;
    }
}