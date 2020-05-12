using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class BuildingBehavior : MonoBehaviour
    {
        private Collider collider;
        private Rigidbody rigidbody;
        private List<Collider> collisions = new List<Collider>();

        private void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = gameObject.AddComponent<Rigidbody>();

            collider.isTrigger = true;
            rigidbody.isKinematic = true;
        }

        public void Place() 
        {
            collider.isTrigger = false;
            Destroy(rigidbody);
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
    }
}
