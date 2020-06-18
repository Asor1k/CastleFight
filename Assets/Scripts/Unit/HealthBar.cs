using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _progressBar;
        private float _colorChangeThreshold = 0.5f;

        public void SetBarValue(float factor)
        {
            _progressBar.fillAmount = factor;

            if (factor > _colorChangeThreshold)
            {
                var colorFactor = (factor - _colorChangeThreshold) / _colorChangeThreshold;

                _progressBar.color = Color.Lerp(Color.yellow, Color.green, colorFactor);
            }
            else
            {
                var colorFactor = factor / _colorChangeThreshold;
                _progressBar.color = Color.Lerp(Color.red, Color.yellow, colorFactor);
            }
        }

        public void Show(bool status)
        {
            gameObject.SetActive(status);
        }
    }
}