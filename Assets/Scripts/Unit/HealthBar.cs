using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CastleFight
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image progressBar;
        [SerializeField]
        private Canvas canvas;
        private float colorChangeThreshold = 0.5f;

        public void SetBarValue(float factor)
        {
            progressBar.fillAmount = factor;

            if (factor > colorChangeThreshold)
            {
                if (progressBar == null) return;
                var colorFactor = (factor - colorChangeThreshold) / colorChangeThreshold;
                
                progressBar.color = Color.Lerp(Color.yellow, Color.green, colorFactor);
            }
            else
            {
                var colorFactor = factor / colorChangeThreshold;
                progressBar.color = Color.Lerp(Color.red, Color.yellow, colorFactor);
            }
        }

        public void Show(bool status)
        {
            canvas.enabled = status;
        }
    }
}