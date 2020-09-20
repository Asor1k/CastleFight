﻿using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CastleFight
{
    public class TalantCard : MonoBehaviour
    {
        public int timeUntilOpened { get; private set; } = -1;

        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI attentionText;
        [SerializeField] private TalantPoolManager poolManager;
        
        private PlayerProgress playerProgress;
        private TalantsGenerator talantsGenerator;
        private int index;
        private bool isReady = false;

        private const int SECONDS_IN_DAY = 60 * 60 * 24;

        public void Start()
        {
            playerProgress = ManagerHolder.I.GetManager<PlayerProgress>();
            talantsGenerator = ManagerHolder.I.GetManager<TalantsGenerator>();
        }

        public void Init(int time, int index)
        {
            Activate();
            this.index = index;
            if (time <= 0)
            {
                ChangeStatus();
                return;
            }
            timeUntilOpened = time;
            ShowTime();
            
            if (poolManager.workingIndex == -1) return;
            if(poolManager.CanWork(index))
                StartCoroutine(TimeCouroutine());
        }

        private void ShowTime()
        {
            timeText.text = IntToRealTimeString(timeUntilOpened);
        }

        public void OnDestroy()
        {
            playerProgress.Data.CardsTimeToOpen[index] = timeUntilOpened;
            playerProgress.Save();
        }

        private string IntToRealTimeString(int number)
        {
            string ans = "";
            bool isHours = false;
            bool isDays = false;
            if (number >= SECONDS_IN_DAY)
            {
                ans += number / SECONDS_IN_DAY + "D ";
                number /= 24;
                isDays = true;
            }
            if (number > SECONDS_IN_DAY / 24)
            {
                ans += number / (SECONDS_IN_DAY / 24) + "h ";
                number %= 60;
                isHours = true;
            }
            if (number >= SECONDS_IN_DAY / 24 / 60)
            {
                ans += number / (SECONDS_IN_DAY / 24 / 60) + "m ";
                number %= 60;
            }
            if(!isHours)
            {
                ans += number + "s";
            }
            return ans;
        }

        public void OnPressed()
        {
            if (!isReady)
            {
                if (index == poolManager.workingIndex) return;
                if (poolManager.CanWork(index))
                {
                    StartCoroutine(TimeCouroutine());
                }    
                return; 
            }
            GenerateTalant();
            Deactivate();
            poolManager.ResetIndex();
        }

        private IEnumerator TimeCouroutine()
        {
            yield return new WaitForSeconds(1);
            timeUntilOpened--;
            ShowTime();
            if (timeUntilOpened <= 0)
            {
                ChangeStatus();
                yield break;
            }
            StartCoroutine(TimeCouroutine());
        }

        private void ChangeStatus()
        {
            timeText.gameObject.SetActive(false);
            isReady = true;
            attentionText.gameObject.SetActive(true);
            timeUntilOpened = -1;
        }

        private void Deactivate()
        {
            timeUntilOpened = 0;
            playerProgress.Data.CardsTimeToOpen[index] = timeUntilOpened;
            playerProgress.Save();
            Debug.Log(index);
            gameObject.SetActive(false);
        }

        private void Activate()
        {
            gameObject.SetActive(true);
            attentionText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(true);
        }

        private void GenerateTalant()
        {
            talantsGenerator.StartGenerating();
        }
    }
}