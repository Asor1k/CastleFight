using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CastleFight
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        public bool IsStopped { get { return agent.isStopped; } }

        [SerializeField]
        protected NavMeshAgent agent;

        public virtual void MoveTo(Vector3 position)
        {
            agent.isStopped = false;
            agent.SetDestination(position);
        }

        public virtual void Stop()
        {
            agent.isStopped = true;
        }
    }
}