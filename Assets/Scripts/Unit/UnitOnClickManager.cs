using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CastleFight
{
    public class UnitOnClickManager : MonoBehaviour
    {
        [SerializeField] private Image circleImg;
        private UserController user;
        private List<SkinnedMeshRenderer> renderers;
        public void Start()
        {
            user = FindObjectOfType<UserController>();
            EventBusController.I.Bus.Subscribe<BuildingDeselectedEvent>(OnDeselect); //TODO: change buildingDeselected to Deselected
            EventBusController.I.Bus.Subscribe<UnitClickedEvent>(UnitClicked);  
        }
        /*  public void OnMouseDown()
          {
              ShowCircle();
              EventBusController.I.Bus.Publish(new UnitClickedEvent(GetComponent<UnitStats>()));
          }
        */

       /* public void OnMouseEnter()
        {
            ShowCircle(user.smallCircle);
        }

        public void OnMouseExit()
        {
            HideCircle();
        }
        */
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<BuildingDeselectedEvent>(OnDeselect); //TODO: change buildingDeselected to Deselected
            EventBusController.I.Bus.Unsubscribe<UnitClickedEvent>(UnitClicked);

        }
        private void UnitClicked(UnitClickedEvent unitClickedEvent)
        {
            Debug.Log("Clicked");
            if (transform==unitClickedEvent.unit.transform)
            ShowCircle(user.bigCircle);
            
        }

        private void ShowCircle(Sprite sprite)
        {
            circleImg.enabled = true;
            circleImg.sprite = sprite;    
        }

        private void OnDeselect(BuildingDeselectedEvent buildingDeselectedEvent)
        {
            HideCircle();
        }

        private void HideCircle()
        {
            circleImg.enabled = false;
        }

    }
}