using CastleFight.Core;
using Core;
using UnityEngine;

public class CoursorBuilder : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private IUpdateManager updateManager;
    private Ray ray;
    private GameObject currentGo;

    private void Start()
    {
        updateManager = ManagerHolder.I.GetManager<IUpdateManager>();
        updateManager.OnUpdate += OnUpdateHandler;
    }

    private void OnDestroy()
    {
        updateManager.OnUpdate -= OnUpdateHandler;
    }

    public void SetBuilding(GameObject go) // TODO: need set to some IBuildingBehavior, which can be moved by this builder
    {
        
    }

    private void OnUpdateHandler()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hit))
            {
                Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), hit.point + new Vector3(0,0.5f, 0), Quaternion.identity); // TODO: temp code
            }
        }
    }
}