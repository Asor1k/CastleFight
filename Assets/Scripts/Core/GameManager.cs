using CastleFight.Core.Data;
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
            BuildingManager.I.Init();
            EventBusController.I.Bus.Subscribe<OpenMainMenuEvent>(OnOpenMainMenuEventHandler);
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameSetReady);
            EventBusController.I.Bus.Subscribe<RestartGameEvent>(OnRestartGame);
        }

        void OnRestartGame(RestartGameEvent restartGameEvent)
        {
            mainMenu = null;
        }

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<OpenMainMenuEvent>(OnOpenMainMenuEventHandler);
            EventBusController.I.Bus.Unsubscribe<GameSetReadyEvent>(OnGameSetReady);
            ManagerHolder.I.Clear();
        }

        private void OnOpenMainMenuEventHandler(OpenMainMenuEvent openMainMenuEvent)
        {
            OpenMainMenu();
        }

        private void OnGameSetReady(GameSetReadyEvent gameSetReadyEvent)
        {
            StartGame(gameSetReadyEvent.gameSet);
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

        private void StartGame(GameSet gameSet)
        {
            gameController.StartGame(gameSet);
        }
    }
}