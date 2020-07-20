using System.Collections;
using System.Collections.Generic;
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
        Destroy
    }
    public class BotBehaviour : MonoBehaviour
    {
        [SerializeField] private RaceConfig raceConfig;
        [SerializeField] private RaceSet raceSet;

        public void Start()
        {
            EventBusController.I.Bus.Subscribe<RaceChosenEvent>(OnRaceChosen);
        }

        private void OnRaceChosen(RaceChosenEvent raceChosenEvent)
        {
            raceConfig = raceChosenEvent.UserRaceConfig == raceSet.RaceConfigs[0] ? raceSet.RaceConfigs[1] : raceSet.RaceConfigs[0];
        }

        public BotAction GetBotAction()
        {
            return BotAction.Build;
        }
     
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<RaceChosenEvent>(OnRaceChosen);
        }

    }
}