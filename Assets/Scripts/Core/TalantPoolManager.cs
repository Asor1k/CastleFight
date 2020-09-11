using System.Collections;
using System;
using System.Collections.Generic;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class TalantPoolManager : MonoBehaviour
    {
        public int workingIndex { get; private set; }

        [SerializeField] private TalantCard[] talantCards;
        [SerializeField] private int standartTime;
        [SerializeField] private int maxOcupied;

        private PlayerProgress playerProgress;

        public void InitNewCard()
        {
            playerProgress.Data.Ticks = DateTime.Now.Ticks;
            maxOcupied = talantCards.Length;
            for(int i = 0; i < maxOcupied; i++)
            {
                if (talantCards[i].timeUntilOpened < 0)
                {
                    talantCards[i].Init(standartTime, i);
                    return;
                }
            }
        }

        public void OnDestroy()
        {
            playerProgress.Data.Ticks = DateTime.Now.Ticks;
            playerProgress.Data.OpeningIndex = workingIndex;
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEnd);
        }

        public void ResetIndex()
        {
            workingIndex = -1;
        }

        private void InitExistingCard(int index)
        {
            if (playerProgress.Data.CardsTimeToOpen[index] <= 0) return;
            
            int timeToSend = playerProgress.Data.CardsTimeToOpen[index];
            if (index == playerProgress.Data.OpeningIndex) timeToSend -= GetSecondsPaseed();
            talantCards[index].Init(timeToSend, index);
        }

        private int GetSecondsPaseed()
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeThen = new DateTime(playerProgress.Data.Ticks);

            long elapsedTicks = dateTimeNow.Ticks - dateTimeThen.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return (int)Math.Round(elapsedSpan.TotalSeconds);
        }

        private void OnGameEnd(GameEndEvent gameEndEvent)
        {
            InitNewCard();
        }

        public bool CanWork(int index)
        {
            if (workingIndex == -1 || index == workingIndex)
            {
                workingIndex = index;
                playerProgress.Data.OpeningIndex = workingIndex;
                return true;
            }
            else return false;
        }

        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }

        public void Start()
        {
            playerProgress = ManagerHolder.I.GetManager<PlayerProgress>();
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
            workingIndex = playerProgress.Data.OpeningIndex;
            for (int i = 0; i < maxOcupied; i++)
            {
                InitExistingCard(i);
            }
        }

    }
}