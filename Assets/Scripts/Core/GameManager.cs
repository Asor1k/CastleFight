using UnityEngine;

namespace CastleFight.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UILayout mainMenu;
        [SerializeField] private GameController gameController;

        private void Start()
        {
            Init();
            // TODO: subscribe to GameSetIsReady and start the game
            // TOOD: subscribe to 
        }

        private void Init()
        {
            OpenMainMenu();
        }

        private void OpenMainMenu()
        {
            mainMenu.Show();
        }

        // private void OnGameSetIsReady() { ... StartGame() }
        
        public void StartGame()
        {
            gameController.StartGame();
        }
    }
}