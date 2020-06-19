using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CastleFight
{
    public class UnitOnClickManager : MonoBehaviour
    {
        [SerializeField] private Image bigCircleImg;
        [SerializeField] private Image smallCircleImg;
        private List<SkinnedMeshRenderer> renderers;
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

        public void OnMouseEnter()
        {
            ShowCircle(smallCircleImg);
        }

        public void OnMouseExit()
        {
            HideCircle(smallCircleImg);
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingDeselectedEvent>(OnDeselect); //TODO: change buildingDeselected to Deselected
            EventBusController.I.Bus.Unsubscribe<UnitClickedEvent>(UnitClicked);

        }
        private void UnitClicked(UnitClickedEvent unitClickedEvent)
        {
            if (transform==unitClickedEvent.unit.transform)
            ShowCircle(bigCircleImg);
        }

        public void ShowCircle(Image image)
        {
            
            image.enabled = true;
        }

        void OnDeselect(BuildingDeselectedEvent buildingDeselectedEvent)
        {
            HideCircle(bigCircleImg);
        }
        public void HideCircle(Image image)
        {
            image.enabled = false;
        }

    }
}