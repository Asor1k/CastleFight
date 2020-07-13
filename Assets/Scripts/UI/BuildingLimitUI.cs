using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight {
    public class BuildingLimitUI : MonoBehaviour
    {
        [SerializeField] private BuildingLimitPopup prefab;
        [SerializeField] private Text limitText;
        [SerializeField] Transform gameUITr;
        private BuildingsLimitManager buildingManager;
        

        public void Start()
        {
            buildingManager = ManagerHolder.I.GetManager<BuildingsLimitManager>();
            buildingManager.UserUpdateLabel += OnUpdateLable;
            buildingManager.UserTryWithMaximum += OnUserTryWithMaximum;
       
        }

        private void OnUserTryWithMaximum()
        {
            BuildingLimitPopup limitPopup = Pool.I.Get<BuildingLimitPopup>();
            if(limitPopup == null)
            {
                limitPopup = Instantiate(prefab,gameUITr);
            }
            else
            {
                limitPopup.gameObject.SetActive(true);
            }
            StartCoroutine(DelayDestroy(limitPopup));
        }
        private void OnUpdateLable()
        {
            limitText.text = buildingManager.BuildingsCount(Team.Team1).ToString() + " / " + buildingManager.BuildingLimitConfig.MaxBuildingsPerTeam;
        }
        
        private IEnumerator DelayDestroy(BuildingLimitPopup limitPopup)
        {
            yield return new WaitForSeconds(1);
            limitPopup.gameObject.SetActive(false);
            Pool.I.Put(limitPopup);
        }
    }
}
