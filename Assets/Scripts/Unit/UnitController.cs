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
        private Damageable currentTarget;
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
            if(currentTarget == null)
            {
                unit.MoveTo(GetEnemyCastlePosition());
            }
            else 
            {
                var distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
                
                if(distanceToTarget <= unit.AttackDistance)
                {
                    unit.Stop();
                    unit.Attack(currentTarget);
                }
                else if(distanceToTarget > unit.EnemyDetectRange)
                    StartCoroutine(UpdateTargetCoroutine());
                else   
                    unit.MoveTo(currentTarget.transform.position);
            }

        }

        private IEnumerator UpdateTargetCoroutine()
        {
            while(true)
            {
                yield return new WaitForSeconds(targetUpdateDelay);
                currentTarget = targetProvider.GetTarget(enemyLayer, transform.position, unit.EnemyDetectRange);

                if(currentTarget != null)
                {
                    StopCoroutine(updateTargetCoroutine);
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