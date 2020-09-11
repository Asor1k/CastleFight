using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

namespace CastleFight
{
    public class DebugButtons : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Text timeText;
        
        private int time = 0;
        private bool isStopped = false;
        public void Start()
        {
            StartCoroutine(TimeCourutine());
            pauseButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Time.timeScale = Convert.ToInt32(isStopped);
            isStopped = !isStopped;
        }

        private IEnumerator TimeCourutine()
        {
            yield return new WaitForSeconds(1f);
            time++;
            timeText.text = time.ToString();
            StartCoroutine(TimeCourutine());
        }

    }
}