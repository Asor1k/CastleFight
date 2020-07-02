using System;
using CastleFight.Config;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.GameUI
{
    public class BuildingButton : MonoBehaviour
    {
        public event Action<BuildingButton> Click;
        
        [SerializeField] private Button btn;
        [SerializeField] private Image img;
        [SerializeField] private Text priceText;
        [SerializeField] private Text nameText;
        private BaseBuildingConfig buildingConfig;
        
        private void Start()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void UpdatePrice()
        {
            priceText.text = buildingConfig.Cost.ToString();
        }

        public void Init(BaseBuildingConfig config)
        {
            buildingConfig = config;
            UpdatePrice();
            nameText.text = config.Name;
            if (config.Icon != null)
            {
                img.sprite = config.Icon;

            }
        }
        
        public void OnClick()
        {
            Click?.Invoke(this);
        }
        public void OnDestroy()
        {
            //Debug.Log("Destroy");
            btn.onClick.RemoveAllListeners();
        }
        public void Destroy()
        {
            // TODO: put to pool
            Destroy(gameObject);
        }
    }
}