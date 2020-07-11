using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace CastleFight.Core {
    public class EndGameUI : UILayout 
    {
        [SerializeField] private Text endText;
        [SerializeField] private Text ratingDeltaText;
        
        [SerializeField] private string winMessage;
        [SerializeField] private string looseMessage;

        [SerializeField] private PlayerProgress playerProgress;

        public void Start()
        {
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
            Hide();
        }
       
        private void OnGameEnd(GameEndEvent gameEndEvent)
        {
            Show();
            if (gameEndEvent.winnerTeam == Team.Team2)
            {
                InitWin();
            }
            else
            {
                InitLoss();
            }
        }
        public void Continue()
        {
            SceneManager.LoadScene(0);
        }
        private void InitWin()
        {
            endText.text = winMessage;
            ratingDeltaText.text = "+"+playerProgress.RatingDelta.ToString();
        }
        private void InitLoss()
        {
            endText.text = looseMessage;
            ratingDeltaText.text = "-" + playerProgress.RatingDelta.ToString();
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEnd);
        }
    }
}