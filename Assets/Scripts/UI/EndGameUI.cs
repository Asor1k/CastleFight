using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using CastleFight.Config;

namespace CastleFight.Core {
    public class EndGameUI : UILayout 
    {
        [SerializeField] private Text ratingDeltaText;
        [SerializeField] private PlayerProgress playerProgress;
        [SerializeField] private RaceConfig[] raceConfigs;

        [SerializeField] private Image[] finalTextImages;
        [SerializeField] private Image[] buttonImages;
        [SerializeField] private Image[] buttonDescriptionImages;
        [SerializeField] private Image unitImage;

        [SerializeField] private Sprite[] finalTextSprites;
        [SerializeField] private Sprite[] unitSprites;
        [SerializeField] private Sprite[] buttonSprites;
        [SerializeField] private Sprite[] decriptionSprites;
        [SerializeField] private RaceSet raceSet;

        private RaceConfig playerRace;
        private AudioManager audioManager;
        public void Start()
        {
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
            EventBusController.I.Bus.Subscribe<RaceChosenEvent>(OnRaceChosen);
            audioManager = ManagerHolder.I.GetManager<AudioManager>();
            Hide();
        }
        
        private void OnRaceChosen(RaceChosenEvent raceChosenEvent)
        {
            playerRace = raceChosenEvent.UserRaceConfig;
        }
        private RaceConfig GetBotConfig()
        {
            foreach (var config in raceSet.RaceConfigs)
            {
                if (!config.Equals(playerRace))
                {
                    return config;
                }
            }

            return null;
        }
        private void OnGameEnd(GameEndEvent gameEndEvent)
        {
            Show();
            if (gameEndEvent.won)
            {
                audioManager.Play("Win");
            }
            else
            {
                audioManager.Play("Loose");
            }
            if (gameEndEvent.loserRace == Race.Kingdom)
            {
                InitImmortals();
            }
            if(gameEndEvent.loserRace == Race.Immortals)
            {
                InitKingdom();
            }
        }

        public void Continue()
        {
            SceneManager.LoadScene(0);
        }
        public void Replay()
        {
            EventBusController.I.Bus.Publish(new GameSetReadyEvent(playerRace, GetBotConfig()));
        }


        private void InitImmortals()
        {
            for(int i = 0; i < 2; i++)
            {
                finalTextImages[i].sprite = finalTextSprites[i];
                buttonImages[i].sprite = buttonSprites[0];
                buttonDescriptionImages[i].sprite = decriptionSprites[i];
            }
            unitImage.sprite = unitSprites[0];
        }
        
        private void InitKingdom()
        {
            for (int i = 0; i < 2; i++)
            {
                finalTextImages[i].sprite = finalTextSprites[i+2];
                buttonImages[i].sprite = buttonSprites[1];
                buttonDescriptionImages[i].sprite = decriptionSprites[i + 2];
            }

            unitImage.sprite = unitSprites[1];
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEnd);
            EventBusController.I.Bus.Unsubscribe<RaceChosenEvent>(OnRaceChosen);
        }
    }
}