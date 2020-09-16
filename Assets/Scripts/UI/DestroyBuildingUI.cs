using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight {
    public class DestroyBuildingUI : BuildingButton
    {
        public void Start()
        {
            //Hide();
        }
        public void OnClick()
        {
            building.SellBuilding();
        }
    }
}