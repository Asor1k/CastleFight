using CastleFight.Config;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight.MainMenu
{
    public class GameSetProvider : MonoBehaviour
    {
        [SerializeField] private RaceSet raceSet;

        private RaceChosenEvent raceChosenEvent;

        private void Awake()
        {
            EventBusController.I.Bus.Subscribe<RaceChosenEvent>(OnRaceChosen);
        }

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<RaceChosenEvent>(OnRaceChosen);
        }

        private void OnRaceChosen(RaceChosenEvent raceChosenEvent)
        {
            this.raceChosenEvent = raceChosenEvent;
        }

        public void PrepareGameSet()
        {
            if (raceChosenEvent?.UserRaceConfig == null)
            {
                Debug.LogError("Race has not been chosen");
                return;
            }

            var botConfig = GetBotConfig();

            if (botConfig == null)
            {
                Debug.LogError("No suitable race config for bot");
                return;
            }

            EventBusController.I.Bus.Publish(new GameSetReadyEvent(raceChosenEvent.UserRaceConfig, botConfig));
            raceChosenEvent = null;
        }

        private RaceConfig GetBotConfig()
        {
            foreach (var config in raceSet.RaceConfigs)
            {
                if (config.Equals(raceChosenEvent.UserRaceConfig) == false)
                {
                    return config;
                }
            }

            return null;
        }
    }
}