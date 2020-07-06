using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight
{
    public class BuildingSelect : MonoBehaviour
    {
        private BuildingBehavior selectedBuilding;
        
        void Start()
        {
            EventBusController.I.Bus.Subscribe<BuildingClickedEvent>(BuildingSelectHandler);
            EventBusController.I.Bus.Subscribe<BuildingDeselectedEvent>(BuildingDeselectHandler);
        }

        private void BuildingSelectHandler(BuildingClickedEvent eventData)
        {
            selectedBuilding = eventData.behavior;
            
            selectedBuilding.Building.Select();
        }

        private void BuildingDeselectHandler(BuildingDeselectedEvent eventData)
        {
            if(selectedBuilding != null)
                selectedBuilding.Building.Deselect();
        }

    }
}