using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CastleFight
{
    public class EveryDayTalant : MonoBehaviour
    {
        [SerializeField] private Button bookButton;
        [SerializeField] private Image bookImage;
        [SerializeField] private Sprite[] bookSprites;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private int standartTime;

        private const int SECONDS_IN_DAY = 60 * 60 * 24;

        private PlayerProgress playerProgress;
        private TalantsGenerator talantsGenerator;
        private int secondsToOpen;
        private bool canBeOpen = false;
        private AudioManager audioManager;
        public void Start()
        {
            playerProgress = ManagerHolder.I.GetManager<PlayerProgress>();
            talantsGenerator = ManagerHolder.I.GetManager<TalantsGenerator>();
            audioManager = ManagerHolder.I.GetManager<AudioManager>();
            bookButton.onClick.AddListener(OnClick);
            bookImage.sprite = bookSprites[0];
            InitTalant();
        }

        public void InitNewTalant()
        {
            secondsToOpen = standartTime;
            audioManager.Play("Book start reserching");
            StartCoroutine(UpdateTimer());
        }

        private void InitTalant()
        {
            secondsToOpen = playerProgress.Data.BookSecondsToOpen - GetSecondsPaseed();
            canBeOpen = false;
            StartCoroutine(UpdateTimer());
            DisplayTime();
        }

        private IEnumerator UpdateTimer()
        {
            yield return new WaitForSeconds(1f);
            secondsToOpen--;
            StartCoroutine(UpdateTimer());
            DisplayTime();
        }

        public void OnDestroy()
        {
            playerProgress.Data.Ticks = DateTime.Now.Ticks;
            playerProgress.Data.BookSecondsToOpen = secondsToOpen;
            playerProgress.Save();
        }

        private void DisplayTime()
        {
            if (secondsToOpen <= 0)
            {
                StopAllCoroutines();
                SetClikable();
            }
            timeText.text = IntToDisplayText(secondsToOpen);
        }

        private void SetClikable()
        {
            canBeOpen = true;
            audioManager.Play("Book researched");
            bookImage.sprite = bookSprites[1];
        }

        private void OnClick()
        {
            if (!canBeOpen) return;
            talantsGenerator.StartGenerating();
            InitNewTalant();
        }

        private string IntToDisplayText(int number)
        {
            string ans = "";

            int t = number / (SECONDS_IN_DAY / 24);
            ans += t >= 10 ? t+"":"0"+t;
            ans += ":";
            number -= t * (SECONDS_IN_DAY / 24);

            t = number / (SECONDS_IN_DAY / 24 / 60);
            ans += t >= 10 ? t + "" : "0" + t;
            ans += ":";
            number -= t*60;
            t = number;
            ans += t >= 10 ? t + "" : "0" + t;

            if (number <= 0)
            {
                ans = "00:00:00";
            }
            return ans;
        }         
        private int GetSecondsPaseed()
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTimeThen = new DateTime(playerProgress.Data.Ticks);

            long elapsedTicks = dateTimeNow.Ticks - dateTimeThen.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return (int)Math.Round(elapsedSpan.TotalSeconds);
        }

    }
}