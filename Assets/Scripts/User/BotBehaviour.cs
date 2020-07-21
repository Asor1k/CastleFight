using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public enum BotAction
    {
        Upgrade,
        Build,
        BuildDestroyed
    }
    public class BotBehaviour : MonoBehaviour
    {
        [SerializeField] private RaceSet raceSet;
        [SerializeField] private BotController botController;

        private RaceConfig raceConfig;
        private GameManager gameManger;
        private GoldManager goldManager;
        public BotAction botAction;


        public void Start()
        {
            EventBusController.I.Bus.Subscribe<RaceChosenEvent>(OnRaceChosen);
            gameManger = ManagerHolder.I.GetManager<GameManager>();
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            botAction = BotAction.Build;
        }


        private void OnRaceChosen(RaceChosenEvent raceChosenEvent)
        {

        }
     
        public bool CanUpgrade()
        {
            if(botAction == BotAction.Upgrade)
            {
                List<Building> upgradbleBuildings = new List<Building>();
                foreach (Building building in botController.Buildings)
                {
                    if (building.Lvl == building.Config.Levels.Count) continue;
                    if(goldManager.BotGoldAmount >= building.Config.Levels[building.Lvl].Cost)
                    {
                        upgradbleBuildings.Add(building);
                    }
                }
                if (upgradbleBuildings.Count == 0) return false;

                botController.UpgradeBuilding(upgradbleBuildings[Random.Range(0, upgradbleBuildings.Count)]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<RaceChosenEvent>(OnRaceChosen);
        }

    }
}