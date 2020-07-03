using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight
{
    public class UnitHighlight : MonoBehaviour
    {
        public void Start()
        {
            EventBusController.I.Bus.Subscribe<BuildingDeselectedEvent>(OnDeselected);
        }

        private void OnDeselected(BuildingDeselectedEvent buildingDeselectedEvent)
        {
            gameObject.SetActive(false);
            Core.Pool.I.Put(this);
        }

        public void Init(Transform tr)
        {
            gameObject.SetActive(true);
            transform.SetParent(tr.GetComponentInChildren<Canvas>().transform);
            transform.localPosition = Vector3.zero;
        }


    }

}