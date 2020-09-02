using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using UnityEngine;

namespace CastleFight
{
    public class BuildingIncome : MonoBehaviour
    {
        [SerializeField] private Building building;

        private void Start()
        {   
            EventBusController.I.Bus.Subscribe<BuildingEarnMoneyEvent>(EarnMoney);
        }

        private void EarnMoney(BuildingEarnMoneyEvent eventData)
        {
            building.GoldManager.MakeGoldChange(building.Config.Levels[building.Lvl-1].GoldPerSecond, (Team)gameObject.layer);
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingEarnMoneyEvent>(EarnMoney);
        }

    }
}