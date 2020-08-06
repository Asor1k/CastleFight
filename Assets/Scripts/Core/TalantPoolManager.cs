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
            playerProgress.Data.ticks = DateTime.Now.Ticks;
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
            playerProgress.Data.ticks = DateTime.Now.Ticks;
            playerProgress.Data.openingIndex = workingIndex;
            EventBusController.I.Bus.Unsubscribe<GameEndEvent>(OnGameEnd);
        }

        public void ResetIndex(int ind)
        {
            if (ind != workingIndex) return;
            workingIndex = -1;
        }

        private void InitExistingCard(int index)
        {
            if (playerProgress.Data.CardsTimeToOpen[index] <= 0) return;
            
            int timeToSend = playerProgress.Data.CardsTimeToOpen[index];
            
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeThen = new DateTime(playerProgress.Data.ticks);
            
            long elapsedTicks = dateTimeNow.Ticks - dateTimeThen.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            int secondsPassed = (int)Math.Round(elapsedSpan.TotalSeconds);

            if (index == -1 || index != workingIndex) secondsPassed = 0;
            talantCards[index].Init(timeToSend - secondsPassed, index);
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
                playerProgress.Data.openingIndex = workingIndex;
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
            workingIndex = playerProgress.Data.openingIndex;
            for (int i = 0; i < maxOcupied; i++)
            {
                InitExistingCard(i);
            }
        }

    }
}