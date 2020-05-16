using System;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.MainMenu
{
    public class RaceButton : MonoBehaviour
    {
        public event Action<RaceConfig> Click;

        [SerializeField] private Button btn;
        [SerializeField] private Text txt;

        private RaceConfig config;

        private void Awake()
        {
            btn.onClick.AddListener(OnBtnClick);
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveAllListeners();
        }

        private void OnBtnClick()
        {
            Click?.Invoke(config);
        }

        public void Init(RaceConfig config)
        {
            this.config = config;
            SetText(config.RaceName);
        }

        private void SetText(string text)
        {
            txt.text = text;
        }
    }
}