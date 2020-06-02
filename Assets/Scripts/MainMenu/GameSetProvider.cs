using CastleFight.Config;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //Begin [Asor1k]
        public void StartNewGame()
        {
            Debug.Log("New game started");
            EventBusController.I.Bus.Publish(new RestartGameEvent());
            SceneManager.LoadScene(0);
            //Restart game
        }
        public void ExitGame()
        {
            Debug.Log("Exit");
            Application.Quit();
            //Exit game
        }
        //End [Asor1k]

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