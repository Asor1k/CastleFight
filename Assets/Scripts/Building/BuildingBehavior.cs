using System;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight
{
    public class BuildingBehavior : MonoBehaviour
    {
        [SerializeField] private Collider col;
        Building building;
        private List<Collider> collisions = new List<Collider>();

        public void Place()
        {
            EventBusController.I.Bus.Publish(new BuildingPlacedEvent(this));
            col.isTrigger = false;
        }

        public void Start()
        {
            
           //Castle castle = gameObject.AddComponent<Castle>();
        }
        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public bool CanBePlaced()
        {
            if (collisions.Count == 0)
                return true;

            return false;
        }

        private void OnTriggerEnter(Collider collider)
        {
            collisions.Add(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            collisions.Remove(collider);
        }

        public void Destroy()
        {
            Destroy(gameObject); // TODO: move to pool
        }
    }
}