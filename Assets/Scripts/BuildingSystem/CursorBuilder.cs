﻿using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using Core;
using UnityEngine;

namespace CastleFight
{
    public class CursorBuilder : UserAbility
    {
        [SerializeField]
        private Team team;
        [SerializeField] private Camera cam;
        [SerializeField] LayerMask buildingAreaLayer;

        private Ray ray;
        private GameObject currentGo;
        [SerializeField] private BuildingBehavior buildingBehavior;

        private void Start()
        {

        }

        private void OnDestroy()
        {
            Lock();
        }

        

        private void SubscribeToBuildingChosenEvent()
        {
            EventBusController.I.Bus.Subscribe<BuildingChosenEvent>(OnBuildingChosen);
        }

        private void UnsubscribeToBuildingChosenEvent()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingChosenEvent>(OnBuildingChosen);
        }

        private void OnBuildingChosen(BuildingChosenEvent buildingChosenEvent)
        {
            Clear();
          
            SetBuilding(buildingChosenEvent.GetBehavior);
        }

        private void Clear()
        {
            if (buildingBehavior != null)
            {
                Debug.Log("Destroy");
                buildingBehavior.Destroy();
            }
        }

        public void SetBuilding(BuildingBehavior buildingBehavior)
        {
            this.buildingBehavior = buildingBehavior;  
          
        }

        private void Update()
        { 
           
            if (buildingBehavior == null) return;
           
            ray = cam.ScreenPointToRay(Input.mousePosition);
        

            if (Physics.Raycast(ray, out var hit, 100, buildingAreaLayer))
            {
                var position = new Vector3(Mathf.RoundToInt(hit.point.x), hit.point.y, Mathf.RoundToInt(hit.point.z));
                buildingBehavior.MoveTo(position);
                
                if (Input.GetMouseButtonDown(0) && buildingBehavior.CanBePlaced())
                {
                    buildingBehavior.Place(team);
                    buildingBehavior = null;
                }
            }
        }

        public override void Unlock()
        {
            SubscribeToBuildingChosenEvent();
        }
       

        public override void Lock()
        {
            UnsubscribeToBuildingChosenEvent();
            Clear();
        }
    }
}