﻿using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using Core;
using UnityEngine;

namespace CastleFight
{
    public class CursorBuilder : UserAbility
    {
        [SerializeField] private Camera cam;
        [SerializeField] LayerMask buildingAreaLayer;

        private IUpdateManager updateManager;
        private Ray ray;
        private GameObject currentGo;
        private BuildingBehavior buildingBehavior;

        private void Start()
        {
            updateManager = ManagerHolder.I.GetManager<IUpdateManager>();
        }

        private void OnDestroy()
        {
            UnsubscribeFromUpdate();
            Lock();
        }

        private void SubscribeToUpdate()
        {
            updateManager.OnUpdate += OnUpdateHandler;
        }

        private void UnsubscribeFromUpdate()
        {
            updateManager.OnUpdate -= OnUpdateHandler;
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
            Debug.Log(buildingChosenEvent.GetBehavior);
            SetBuilding(buildingChosenEvent.GetBehavior);
        }

        private void Clear()
        {
            if (buildingBehavior != null)
            {
                buildingBehavior.Destroy();
            }
        }

        public void SetBuilding(BuildingBehavior buildingBehavior)
        {
            this.buildingBehavior = buildingBehavior;
        }

        private void OnUpdateHandler()
        {
            if (buildingBehavior == null) return;

            ray = cam.ScreenPointToRay(Input.mousePosition);
           // Debug.Log("OnUpdate");
            if (Physics.Raycast(ray, out var hit, 100, buildingAreaLayer))
            {
                var position = new Vector3(Mathf.RoundToInt(hit.point.x), hit.point.y, Mathf.RoundToInt(hit.point.z));
                buildingBehavior.MoveTo(position);
                
                if (Input.GetMouseButtonDown(0) && buildingBehavior.CanBePlaced())
                {
                    
                    buildingBehavior.Place();
                    buildingBehavior = null;
                }
            }
        }

        public override void Unlock()
        {
            SubscribeToUpdate();
            SubscribeToBuildingChosenEvent();
        }
       

        public override void Lock()
        {
            UnsubscribeFromUpdate();
            UnsubscribeToBuildingChosenEvent();
            Clear();
        }
    }
}