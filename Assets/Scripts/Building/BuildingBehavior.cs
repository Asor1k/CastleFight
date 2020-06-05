using System;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight
{
    public class BuildingBehavior : MonoBehaviour
    {
        public Team Team => team;
        [SerializeField] private Collider col;
        [SerializeField] Building building;
        public float offsetY;
        private List<Collider> collisions = new List<Collider>();
        private MeshRenderer rend;
        private Team team;

        public void Place(Team team)
        {
            this.team = team;
            gameObject.layer = (int)team;
            EventBusController.I.Bus.Publish(new BuildingPlacedEvent(this));
            col.isTrigger = false;

            building.Build();
        }

        public void Start()
        {
            rend = GetComponent<MeshRenderer>();
           //Castle castle = gameObject.AddComponent<Castle>();
        }
        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public bool CanBePlaced()
        {
            if (rend == null) return true;
            if (collisions.Count == 0)  
            {
                rend.material.color = Color.white; //Make color changes using shaders
                return true;
            }
            rend.material.color = Color.red;
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