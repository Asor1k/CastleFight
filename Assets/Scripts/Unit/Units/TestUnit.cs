using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class TestUnit : Unit
    {
        //[Asor1k]
        private void Start()
        {
            agent = GetComponent<Agent>();
            
            agent.MoveTo(Vector3.zero);
        }
    }
}