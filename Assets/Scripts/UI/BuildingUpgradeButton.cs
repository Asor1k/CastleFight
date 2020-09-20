using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CastleFight.Config;

namespace CastleFight.UI
{
    public class BuildingUpgradeButton : BuildingButton
    {
        public bool IsInited => isInited;
        [SerializeField] private Text costLabel;
        [SerializeField] private Image icon;
        private bool isInited;


        public void SetLabels(Sprite iconImage, int cost)
        {
            costLabel.text = cost.ToString();
            icon.sprite = iconImage;
        }

        public void SetInited(bool status)
        {
            isInited = status;
        }

        public void SetBuilding(Building building)
        {
            this.building = building;
        }

        public void OnButtonClick(int nodeIndex)
        {
            building.UpgradeBuilding(nodeIndex);
        }

    }
}
