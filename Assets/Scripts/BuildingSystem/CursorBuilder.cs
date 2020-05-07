using CastleFight.Core;
using Core;
using UnityEngine;

namespace CastleFight
{
    public class CursorBuilder : UserAbility
    {
        [SerializeField] private Camera cam;

        private IUpdateManager updateManager;
        private Ray ray;
        private GameObject currentGo;

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

        public void
            SetBuilding(GameObject go) // TODO: need set to some IBuildingBehavior, which can be moved by this builder
        {
        }

        private void OnUpdateHandler()
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position =
                        hit.point + new Vector3(0, 0.5f, 0); // TODO: temp code
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