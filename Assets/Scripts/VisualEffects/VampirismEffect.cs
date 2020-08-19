using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class VampirismEffect : VisualEffect
    {
        protected override void OnEnd()
        {
            gameObject.SetActive(false);
            Pool.I.Put<VampirismEffect>(this);
        }
    }
}