using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CastleFight
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        public bool IsStopped { get { return _agent.isStopped; } }

        [SerializeField]
        protected NavMeshAgent _agent;

        public virtual void MoveTo(Vector3 position)
        {
            _agent.isStopped = false;
            _agent.SetDestination(position);
        }

        public virtual void Stop()
        {
            _agent.isStopped = true;
        }
    }
}