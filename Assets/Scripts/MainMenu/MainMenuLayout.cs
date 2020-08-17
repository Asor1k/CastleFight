using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CastleFight.MainMenu
{
    public class MainMenuLayout : UILayout
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button handButton;
        [SerializeField] private Button arrowButton;
        [SerializeField] private GameSetProvider setProvider;
        [SerializeField] private Canvas gameSetup;
        [SerializeField] private Canvas startPart;
        [SerializeField] private Canvas talantsCanvas;
        [SerializeField] private TalantScreenManager talantScreenManager;
        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayClick);
            handButton.onClick.AddListener(OnHandPressed);
            arrowButton.onClick.AddListener(OnArrowPressed);
            
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameSetReady);
            EventBusController.I.Bus.Subscribe<RestartGameEvent>(OnRestartGame);
            SetGameSetupCanvasEnabled(true);
        }

        private void OnRestartGame(RestartGameEvent restartGameEvent)
        {
            /*playButton = null;
            setProvider = null;
            gameSetup = null;
            startPart = null;
            Destroy(this);*/
        }

        private void OnHandPressed()
        {
            SetGameSetupCanvasEnabled(false);
            SetTalantsCanvasEnabled(true);
            talantScreenManager.Init();
        }

        private void OnArrowPressed()
        {
            SetGameSetupCanvasEnabled(true);
            SetTalantsCanvasEnabled(false);
        }

        private void SetTalantsCanvasEnabled(bool enabled)
        {
            talantsCanvas.enabled = enabled;
        }

        private void SetGameSetupCanvasEnabled(bool enabled)
        {
            if(gameSetup == null)
            {
                gameSetup = GetComponentInChildren<Canvas>();
            }
            gameSetup.enabled = enabled;
        }

        
        private void OnGameSetReady(GameSetReadyEvent gameSetReady)
        {
            SetGameSetupCanvasEnabled(false);
        }

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameSetReadyEvent>(OnGameSetReady);
            EventBusController.I.Bus.Unsubscribe<RestartGameEvent>(OnRestartGame);
            playButton.onClick.RemoveAllListeners();
        }

        private void OnPlayClick()
        {
            setProvider.PrepareGameSet();
        }

        
        public override void Show()
        {
            startPart.enabled = true;
            base.Show();
        }
    }
}