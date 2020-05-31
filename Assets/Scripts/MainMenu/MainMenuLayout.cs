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
        [SerializeField] private GameSetProvider setProvider;
        [SerializeField] private Canvas gameSetup;
        [SerializeField] private Canvas startPart;
        
        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayClick);
            DontDestroyOnLoad(this);    
            
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameSetReady);

            SetGameSetupCanvasEnabled(true); //[Asor1k] Changed on true
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