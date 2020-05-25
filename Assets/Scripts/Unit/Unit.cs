using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField]
        protected Agent agent; 
    }
}