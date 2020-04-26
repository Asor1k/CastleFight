using UnityEngine;

namespace CastleFight.Core.Extensions
{
    public static class VectorExtensions
    {
        public static void Set(this ref Vector3 v, Vector3 newVector)
        {
            v.Set(newVector.x, newVector.y, newVector.z);
        }
    }
}