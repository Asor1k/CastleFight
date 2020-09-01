using CastleFight.Core.Data;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace CastleFight.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UserController userController;
        [SerializeField] private BotController botController;

        private AudioManager audioManager;
        private string currentTheme;
        

        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
            EventBusController.I.Bus.Subscribe<ExitToMainMenuEvent>(OnExitToMainMenuEventHandler);
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEndEventHandler);
        }

        public void Start()
        {
            audioManager = ManagerHolder.I.GetManager<AudioManager>();
            PlayMenuTheme();
            StartCoroutine(MenuThemeCourutine());
        }

        private IEnumerator MenuThemeCourutine()
        {
            yield return new WaitForSeconds(audioManager.GetLength(currentTheme));
            PlayMenuTheme();
            StartCoroutine(MenuThemeCourutine());
        }

        private void PlayMenuTheme()
        {
            currentTheme = "Menu Theme"+Random.Range(1, 4);
            audioManager.Play(currentTheme); 
            Debug.Log("Started playing" + currentTheme);
        }

        private IEnumerator BattleThemeCourutine(int themeNumber)
        {
            currentTheme = "Battle Theme"+themeNumber;
            audioManager.Play(currentTheme);
            yield return new WaitForSeconds(audioManager.GetLength(currentTheme));
            themeNumber = themeNumber == 3 ? 1 : themeNumber + 1;
            StartCoroutine(BattleThemeCourutine(themeNumber));
        }

        private void StopTheme()
        {
            audioManager.Stop(currentTheme);
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
            StopTheme();
            audioManager.Play("Battle begins");
            StartCoroutine(BattleThemeCourutine(1));
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
            audioManager.Stop(currentTheme);
        }
    
        private void RiseOpenMainMenu()
        {
            EventBusController.I.Bus.Publish(new OpenMainMenuEvent());
        }
    }
}