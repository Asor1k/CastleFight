using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;
        private TargetProvider targetProvider = new TargetProvider();
        private IDamageable currentTarget;
        private LayerMask enemyLayer;
        private float targetUpdateDelay = 0.5f;
        private Coroutine updateTargetCoroutine;

        private void Awake()
        {
            unit.OnInit += OnUnitInitted;
        }

        private void OnUnitInitted()
        {
            if(gameObject.layer == (int)Team.Team1){
                enemyLayer = LayerMask.GetMask("Team2");
            }
            else{
                enemyLayer = LayerMask.GetMask("Team1");
            }

            updateTargetCoroutine = StartCoroutine(UpdateTargetCoroutine());
        }

        void Update()
        {
            if (!unit.Alive)
            {
                if (updateTargetCoroutine != null)
                {
                    StopCoroutine(updateTargetCoroutine);
                    updateTargetCoroutine = null;
                }

                return;
            }
            
            if(currentTarget == null)
            {
                unit.MoveTo(GetEnemyCastlePosition());
            }
            else 
            {
                if(!currentTarget.Alive)
                {
                    currentTarget = null;
                    return;
                }

                var distanceToTarget = Vector3.Distance(transform.position, currentTarget.Transform.position);
                var targetScale = currentTarget.Transform.localScale;
                distanceToTarget -= GetInnerObgectSize(targetScale, distanceToTarget);
                
                if(distanceToTarget <= unit.AttackDistance)
                {
                    unit.Stop();
                    unit.Attack(currentTarget);
                }
                else if (distanceToTarget > unit.EnemyDetectRange)
                    currentTarget = null;
                else   
                    unit.MoveTo(currentTarget.Transform.position);
            }

        }

        private float GetInnerObgectSize(Vector3 targetScale, float distance)
        {
            return distance * (1 - (transform.position.z - targetScale.z / 2) / transform.position.z);
        }
        private IEnumerator UpdateTargetCoroutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(targetUpdateDelay);
                var target = targetProvider.GetTarget(enemyLayer, transform.position, unit.EnemyDetectRange);

                if(target != null)
                {
                    currentTarget = target;
                }
            }
        }

        private Vector3 GetEnemyCastlePosition()
        {
            if(gameObject.layer == (int)Team.Team1)
                return new Vector3(0,0,30);
            else
                 return new Vector3(0,0, -30);
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}