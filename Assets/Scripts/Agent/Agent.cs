using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CastleFight
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Agent : MonoBehaviour
    {
        public float Speed{get{return agent.velocity.magnitude;}}
        public bool IsStopped { get { return agent.isStopped; } }

        [SerializeField]
        protected NavMeshAgent agent;
        [SerializeField]
        protected NavMeshObstacle obstacle;

        public virtual void MoveTo(Vector3 position)
        {
            obstacle.enabled = false;
            agent.enabled = true;

            if (!gameObject.activeSelf || !agent.isOnNavMesh) return;


            agent.isStopped = false;
            agent.SetDestination(position);

        }

        public virtual void LookAt(Transform transform)
        {
            RotateTowards(transform);
        }
        
        public virtual void Stop()
        {
            agent.enabled = false;
            obstacle.enabled = true;
        }

        public void Disable()
        {
            agent.enabled = false;
            obstacle.enabled = false;
        }

        public void Enable()
        {
            agent.enabled = true;
            obstacle.enabled = false;
        }
        private void RotateTowards (Transform target) {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);
        }
    }
}