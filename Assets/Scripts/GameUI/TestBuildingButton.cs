using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class TestBuildingButton : MonoBehaviour
    {
        [SerializeField]
        private BaseBuildingConfig buildingConfig;
        [SerializeField]
        private CursorBuilder cursorBuilder;

        public void OnClick()
        {
            var building = buildingConfig.Create();
            cursorBuilder.SetBuilding(building.GetComponent<BuildingBehavior>());
        }
    }
}