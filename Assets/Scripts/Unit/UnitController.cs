using CastleFight.Core;
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
        private UnitManager unitManager;

        private void Awake()
        {
            unit.OnInit += OnUnitInitted;
        }

        private void Start()
        {
            unitManager = ManagerHolder.I.GetManager<UnitManager>();
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
                    var targetTransform = currentTarget.Transform;
                    distanceToTarget -= Mathf.Max(0, targetTransform.localScale.x / 2);
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
                var target = unitManager.GetClossestUnit(transform.position, enemyDetectRange.Value, (Team)gameObject.layer, ignoreAir);

                if (target != null)
                {
                    currentTarget = target;
                }
                else
                {
                    var buildingTarget = unitManager.GetClossestBuilding(transform.position, enemyDetectRange.Value, (Team)gameObject.layer);

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