using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CastleFight.UI
{
    public class BuildingUpgradeButton : BuildingButton
    {
        [SerializeField] private Text costLabel;
        public Image myImage;

        public void Start()
        {
            //Hide();
        }
        public void SetCostLabel(string cost)
        {
            costLabel.text = cost;
        }

        public void OnButtonClick()
        {
            building.UpgradeBuilding();
        }

    }
}
