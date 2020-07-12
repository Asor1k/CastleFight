using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CastleFight.UI
{
    public class BuildingUpgradeButton : MonoBehaviour
    {
        [SerializeField] private Text costLabel;
        [SerializeField] private Building building;

        public void Start()
        {
            Hide();
        }
        public void SetCostLabel(string cost)
        {
            costLabel.text = cost.ToString();
        }

        public void OnButtonClick()
        {
            building.UpgradeBuilding();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
