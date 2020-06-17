using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace CastleFight
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private List<BotBuildStep> buildSteps;
        [SerializeField] List<Transform> buildingsTrList;
        [SerializeField] private CastlesPosProvider castlesPosProvider;
        [SerializeField] float turnTime;
        [SerializeField] Vector3 stepOffset;
        [SerializeField] private int goldPerTurn = 0;
        [SerializeField] private int gold;
        [SerializeField] private int currBuildIndex;
        private int blocksBuilt = 0;
        [SerializeField] int maxBlocksBuilt;
        public void Init(RaceConfig config)
        {
            CreateCastle(config.CastleConfig);
            StartGame();
        }

        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y, castlePos.z); // TODO: remove magic number
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
            castle.gameObject.layer = (int)Team.Team2;
        }

        private void PlaceBuilding(int buildInd)
        {
            var building = buildSteps[buildInd].BuildingConfig.Create();
            building.transform.position = buildSteps[buildInd].Point.position + stepOffset*blocksBuilt;
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
            gold += goldPerTurn;
            if (gold >= buildSteps[currBuildIndex].BuildingConfig.Cost)
            {
                PlaceBuilding(currBuildIndex);              
                gold -= buildSteps[currBuildIndex].BuildingConfig.Cost;
                goldPerTurn += buildSteps[currBuildIndex].BuildingConfig.GoldIncome;
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
    }

    [Serializable]
    public class BotBuildStep
    {
        public Transform Point;
        public BaseBuildingConfig BuildingConfig;
    }
}