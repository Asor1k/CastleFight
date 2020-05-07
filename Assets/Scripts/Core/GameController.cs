using UnityEngine;

namespace CastleFight.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UserController userController;
        
        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        private void Start()
        {
            // TODO: subscribe to exit to main menu event
        }

        public void StartGame()
        {
            // TODO: start bot
            userController.StartGame();
        }

        private void OnExitToMainMenuEventHandler()
        {
            userController.StopGame();
            // TODO: stop bot
            // TODO: rise OpenMainMenuEvent
        }
    }
}

