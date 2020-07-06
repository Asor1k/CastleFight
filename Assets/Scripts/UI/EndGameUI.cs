using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus.Events;
using UnityEngine.UI;
using UnityEngine;
using CastleFight.Core.EventsBus;

namespace CastleFight.Core {
    public class EndGameUI : UILayout 
    {
        [SerializeField] private Text endText;

        [SerializeField] private string winMessage;
        [SerializeField] private string looseMessage;


        public void Start()
        {
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
            
            Hide();
        }
       
        private void OnGameEnd(GameEndEvent gameEndEvent)
        {
            Show();
            if (gameEndEvent.winnerTeam == Team.Team1)
            {
                InitWin();
            }
            else
            {
                InitLoss();
            }
        }
        
        private void InitWin()
        {
            endText.text = winMessage;
        }
        private void InitLoss()
        {
            endText.text = looseMessage;
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEnd);
        }
    }
}