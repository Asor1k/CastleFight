using System;
using CastleFight.Config;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace CastleFight.GameUI
{
    public class BuildingButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<BuildingButton> Click;
        
        [SerializeField] private Button btn;
        [SerializeField] private Image img;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image priceFrame;
        [SerializeField] private Text nameText;
        private BaseBuildingConfig buildingConfig;
        private bool isBuilding = false;

        public void OnPointerExit(PointerEventData eventData)
        {
            isBuilding = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isBuilding = true;
        }
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(0) && isBuilding)
            {
                OnClick();
            }
        }
        
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
                priceFrame.sprite = config.Frame;
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