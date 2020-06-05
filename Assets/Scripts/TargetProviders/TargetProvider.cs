using UnityEngine;

namespace CastleFight
{
    public class TargetProvider
    {
        public Damageable GetTarget(LayerMask targetLayer,Vector3 originPoint, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(originPoint, radius, targetLayer);

            foreach(var collider in hitColliders)
            {
                var damageable = collider.GetComponent<Damageable>();
                if(damageable != null)
                    return damageable;
            }

            return null;
        }
    }
}
