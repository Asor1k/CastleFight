using System;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.GameUI
{
    public class BuildingButton : MonoBehaviour
    {
        public event Action<BuildingButton> Click;
        
        [SerializeField] private Button btn;
        [SerializeField] private Image img;

        private void Start()
        {
            btn.onClick.AddListener(OnClick);
        }

        public void Init(BaseBuildingConfig config)
        {
            if (config.Icon != null)
            {
                img.sprite = config.Icon;
            }
        }
        
        public void OnClick()
        {
            Click?.Invoke(this);
        }

        public void Destroy()
        {
            // TODO: put to pool
            Destroy(gameObject);
        }
    }
}