using CastleFight.Core.Data;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UserController userController;
        [SerializeField] private BotController botController;

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
            EventBusController.I.Bus.Subscribe<ExitToMainMenuEvent>(OnExitToMainMenuEventHandler);
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEndEventHandler);
        }

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<ExitToMainMenuEvent>(OnExitToMainMenuEventHandler);
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEndEventHandler);
        }

        public void StartGame(GameSet gameSet)
        {
            botController.Init(gameSet.botRaceConfig);
            userController.Init(gameSet.userRaceConfig);
            userController.StartGame();
            // TODO: start bot            
        }

        private void StopGame()
        {
            userController.StopGame();
            // TODO: stop bot
        }

        private void OnExitToMainMenuEventHandler(ExitToMainMenuEvent exitToMainMenuEvent)
        {
            StopGame();
            RiseOpenMainMenu();
        }

        private void OnGameEndEventHandler(GameEndEvent gameEndEvent)
        {
            StopGame();
            RiseOpenMainMenu();
        }
    
        private void RiseOpenMainMenu()
        {
            EventBusController.I.Bus.Publish(new OpenMainMenuEvent());
        }
    }
}