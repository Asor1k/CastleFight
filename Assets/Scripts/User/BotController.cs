using System;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private List<BotBuildStep> buildSteps;
        [SerializeField] private CastlesPosProvider castlesPosProvider;

        public void Init(RaceConfig config)
        {
            CreateCastle(config.CastleConfig);
            PlaceTestBuilding();
        }

        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y + 2.5f, castlePos.z); // TODO: remove magic number
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
            castle.gameObject.layer = (int)Team.Team2;
        }

        private void PlaceTestBuilding()
        {
            var building = buildSteps[0].BuildingConfig.Create();
            building.transform.position = buildSteps[0].Point.position;
            building.GetComponent<BuildingBehavior>().Place(Team.Team2);
        }

        public void StartGame()
        {
 
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