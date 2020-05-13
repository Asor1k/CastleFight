using CastleFight.Core;
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
        }

        private void SubscribeToUpdate()
        {
            updateManager.OnUpdate += OnUpdateHandler;
        }

        private void UnsubscribeFromUpdate()
        {
            updateManager.OnUpdate -= OnUpdateHandler;
        }

        public void SetBuilding(BuildingBehavior buildingBehavior)
        {
            this.buildingBehavior = buildingBehavior;
        }

        private void OnUpdateHandler()
        {
            if (buildingBehavior == null) return;

            ray = cam.ScreenPointToRay(Input.mousePosition);

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
        }

        public override void Lock()
        {
            UnsubscribeFromUpdate();
        }
    }
}