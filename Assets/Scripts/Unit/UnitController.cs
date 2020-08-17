using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;
        [SerializeField]
        private bool ignoreAir;
        private TargetProvider targetProvider = new TargetProvider();
        private IDamageable currentTarget;
        private LayerMask enemyLayer;
        private float targetUpdateDelay = 0.1f;
        private Coroutine updateTargetCoroutine;
        private Stat attackRange;
        private Stat enemyDetectRange;

        private void Awake()
        {
            unit.OnInit += OnUnitInitted;
        }

        private void OnUnitInitted()
        {
            if (gameObject.layer == (int)Team.Team1)
            {
                enemyLayer = LayerMask.GetMask("Team2");
            }
            else
            {
                enemyLayer = LayerMask.GetMask("Team1");
            }

            attackRange = (Stat)unit.Stats.GetStat(StatType.AttackRange);
            enemyDetectRange = (Stat)unit.Stats.GetStat(StatType.EnemyDetectRange);

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

            if (currentTarget == null)
            {
                unit.MoveTo(GetEnemyCastlePosition());
            }
            else
            {
                if (!currentTarget.Alive)
                {
                    currentTarget = null;
                    return;
                }

                var distanceToTarget = Vector3.Distance(transform.position, currentTarget.Transform.position);

                if (currentTarget.Type == TargetType.Building || currentTarget.Type == TargetType.Castle)
                {
                    var targetTransform = currentTarget.Transform.localScale;
                    distanceToTarget -= Mathf.Max(0, currentTarget.Transform.localScale.x - attackRange.Value);
                }

                if (distanceToTarget <= attackRange.Value)
                {
                    unit.Stop();
                    unit.Attack(currentTarget);
                }
                else if (distanceToTarget > enemyDetectRange.Value)
                    currentTarget = null;
                else
                    unit.MoveTo(currentTarget.Transform.position);
            }

        }

        private IEnumerator UpdateTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(targetUpdateDelay);
                var target = UnitManager.I.GetClossestUnit(transform.position, (Team)gameObject.layer, ignoreAir);

                if (target != null)
                {
                    currentTarget = target;
                }
                else
                {
                    var buildingTarget = UnitManager.I.GetClossestBuilding(transform.position, (Team)gameObject.layer);

                    if (buildingTarget != null)
                    {
                        currentTarget = buildingTarget;
                    }
                }
            }
        }

        private Vector3 GetEnemyCastlePosition()
        {
            if (gameObject.layer == (int)Team.Team1)
                return new Vector3(0, 0, 30);
            else
                return new Vector3(0, 0, -30);
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}