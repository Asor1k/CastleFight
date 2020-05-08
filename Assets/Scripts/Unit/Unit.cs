using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class Unit : MonoBehaviour
    {
        [SerializeField]
        private Agent _agent;

        private void Start()
        {
            _agent = GetComponent<Agent>();
        }  
    }
}