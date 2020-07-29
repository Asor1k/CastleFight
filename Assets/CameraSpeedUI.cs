using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CastleFight
{
    public class CameraSpeedUI : MonoBehaviour
    {
        [SerializeField] private Button plusButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private int step;
        [SerializeField] private Text speedText;
        [SerializeField] private CameraMoverSettings moverSettings;

        public void Start()
        {
            speedText.text = "Camera speed: " + moverSettings.Speed.ToString();
            plusButton.onClick.AddListener(OnPlussPressed);
            minusButton.onClick.AddListener(OnMinusPressed);
        }

        private void OnMinusPressed()
        {
            moverSettings.Speed -= step;
            speedText.text = "Camera speed: " + moverSettings.Speed.ToString();
        }

        private void OnPlussPressed()
        {
            moverSettings.Speed += step;

            speedText.text = "Camera speed: " + moverSettings.Speed.ToString();
        }

        public void OnDestroy()
        {
            minusButton.onClick.RemoveAllListeners();
            plusButton.onClick.RemoveAllListeners();
        }
    }
}