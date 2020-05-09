using System;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UILayout mainMenu;
        [SerializeField] private GameController gameController;

        private void Awake()
        {
            EventBusController.I.Bus.Subscribe<OpenMainMenuEvent>(OnOpenMainMenuEventHandler);
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameSetREady);
        }

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<OpenMainMenuEvent>(OnOpenMainMenuEventHandler);
            EventBusController.I.Bus.Unsubscribe<GameSetReadyEvent>(OnGameSetREady);
        }

        private void OnOpenMainMenuEventHandler(OpenMainMenuEvent openMainMenuEvent)
        {
            OpenMainMenu();
        }
        
        private void OnGameSetREady(GameSetReadyEvent gameSetReadyEvent)
        {
            StartGame();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            mainMenu.Show();
        }

        private void StartGame()
        {
            gameController.StartGame();
        }
    }
}