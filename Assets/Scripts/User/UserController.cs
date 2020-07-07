using CastleFight.Core;
using CastleFight.GameUI;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class UserController : MonoBehaviour
    {
        [SerializeField] private UserAbility[] abilities;
        [SerializeField] private GameUIBehavior gameUILayoutPrefab;
        [SerializeField] internal RectTransform gameUIHolder;
        [SerializeField] private CastlesPosProvider castlesPosProvider;
        private GameUIBehavior gameUI;
        private GoldManager goldManager;
        private Ray ray;
        [SerializeField] Camera cam;
       
        public void Init(RaceConfig config)
        {
            if (gameUI == null)
            {
                gameUI = Instantiate(gameUILayoutPrefab, gameUIHolder);
            }
            gameUI.Hide();
            gameUI.Init(config.BuildingSet);

            CreateCastle(config.CastleConfig);
        }
        private void OnUnitDie(UnitDiedEvent unitDiedEvent)
        {
            if (unitDiedEvent.Unit.gameObject.layer == (int)Team.Team2)
            {
                goldManager.MakeGoldChange(unitDiedEvent.Unit.Config.Cost);
            }
            else
            {
                //DO something when your unit dies
            }
        }
        public void Start()
        {
            goldManager = ManagerHolder.I.GetManager<GoldManager>();
            EventBusController.I.Bus.Subscribe<UnitDiedEvent>(OnUnitDie);
        }
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 100))
                {
                    if (hit.collider.CompareTag("Building"))
                    {
                        if (hit.collider.gameObject.layer != (int)Team.Team1) return;
                        EventBusController.I.Bus.Publish(new BuildingClickedEvent(hit.collider.GetComponent<BuildingBehavior>()));
                    }
                    else if (hit.collider.CompareTag("Unit"))
                    {

                        EventBusController.I.Bus.Publish(new BuildingDeselectedEvent());
                        EventBusController.I.Bus.Publish(new UnitClickedEvent(hit.collider.GetComponent<UnitStats>()));
                    }
                    else if(!IsRaycastUI())
                    {
                        EventBusController.I.Bus.Publish(new BuildingDeselectedEvent());
                    }
                }

            }
        }

        private bool IsRaycastUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            bool isUi = results.Count != 0;
            results.Clear();
            return isUi;
        }
        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<UnitDiedEvent>(OnUnitDie);
        }
        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y, castlePos.z);
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
            castle.gameObject.layer = (int)Team.Team1;
            castle.Init(castleConfig);
        }

        public void StartGame()
        {
            UnlockAbilities();
            gameUI.Show();
        }

        public void StopGame()
        {
            LockAbilities();
            gameUI.Hide();
        }

        private void UnlockAbilities()
        {
            foreach (var ability in abilities)
            {
                ability.Unlock();
            }
        }

        private void LockAbilities()
        {
            foreach (var ability in abilities)
            {
                ability.Lock();
            }
        }
    }
}