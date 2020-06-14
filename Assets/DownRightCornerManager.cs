using UnityEngine;
using UnityEngine.UI;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using CastleFight;

namespace Castlefight {
    public class DownRightCornerManager : MonoBehaviour
    {
        [SerializeField] private Text lvlText;
        [SerializeField] private Text buildingNameText;
        [SerializeField] private Image buildingIcon;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Text upgrText;
        private BuildingBehavior buildingBehaviour;
        private Canvas canvas;
        public void Start()
        {
            canvas = GetComponent<Canvas>();
            EventBusController.I.Bus.Subscribe<BuildingClickedEvent>(OnBuildingClicked);
            EventBusController.I.Bus.Subscribe<BuildingDeselectedEvent>(OnBuildingDeselected);
            upgradeButton.onClick.AddListener(OnUpgrade);
        }

        private void OnUpgrade()
        {
            buildingBehaviour.building.UpgradeBuilding();
            UpdateGUI();
        }
        private void OnBuildingClicked(BuildingClickedEvent buildingClicked)
        {
            canvas.enabled = true;
            buildingBehaviour = buildingClicked.behavior;
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            upgrText.text = "Upgrade: " + buildingBehaviour.building.Config.Cost.ToString();
            buildingNameText.text = buildingBehaviour.building.Config.Name;
            buildingIcon.sprite = buildingBehaviour.building.Config.Icon;
            lvlText.text = "Lvl: " + buildingBehaviour.building.Lvl.ToString();
        }
        private void OnBuildingDeselected(BuildingDeselectedEvent buildingDeselectedEvent)
        {
            canvas.enabled = false;
        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingClickedEvent>(OnBuildingClicked);
            EventBusController.I.Bus.Unsubscribe<BuildingDeselectedEvent>(OnBuildingDeselected);
            upgradeButton.onClick.RemoveAllListeners();
        }
    }
}