using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class TestUnit : Unit
    {
        private void Start()
        {
            agent.MoveTo(Vector3.zero);
        }
    }
}