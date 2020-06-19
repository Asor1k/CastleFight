using UnityEngine;

namespace CastleFight
{
    public class TargetProvider
    {
        public IDamageable GetTarget(LayerMask targetLayer,Vector3 originPoint, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(originPoint, radius, targetLayer);

            IDamageable closest = null;
            float closestDistance = -1;
            
            foreach(var collider in hitColliders)
            {
                var damageable = collider.GetComponent<IDamageable>();
                
                if (damageable != null)
                {
                    var distance = Vector2.Distance(new Vector2(originPoint.x, originPoint.z),
                        new Vector2(damageable.Transform.position.x, damageable.Transform.position.z));

                    if (closest != null && closest.Type == TargetType.Building && damageable.Type != TargetType.Building)
                    {
                        closest = damageable;
                        closestDistance = distance;
                    }
                    else if (closestDistance == -1 || closestDistance > distance)
                    {
                        closest = damageable;
                        closestDistance = distance;
                    }
                }

            }

            return closest;
        }
    }
}
