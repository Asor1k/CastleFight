using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using UnityEngine.UI;
using UnityEngine;

namespace CastleFight
{
    public class UnitOnClickManager : MonoBehaviour
    {
        [SerializeField] private Image circleImg;
        public void Start()
        {
            EventBusController.I.Bus.Subscribe<BuildingDeselectedEvent>(OnDeselect); //TODO: change buildingDeselected to Deselected
            EventBusController.I.Bus.Subscribe<UnitClickedEvent>(UnitClicked);
        }
        /*  public void OnMouseDown()
          {
              ShowCircle();
              EventBusController.I.Bus.Publish(new UnitClickedEvent(GetComponent<UnitStats>()));
          }
        */
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingDeselectedEvent>(OnDeselect); //TODO: change buildingDeselected to Deselected
            EventBusController.I.Bus.Unsubscribe<UnitClickedEvent>(UnitClicked);

        }
        private void UnitClicked(UnitClickedEvent unitClickedEvent)
        {
            if (transform==unitClickedEvent.unit.transform)
            ShowCircle();
        }

        public void ShowCircle()
        {
            Debug.Log("clicked");
            circleImg.enabled = true;
        }

        void OnDeselect(BuildingDeselectedEvent buildingDeselectedEvent)
        {
            HideCircle();
        }
        public void HideCircle()
        {
            circleImg.enabled = false;
        }

    }
}