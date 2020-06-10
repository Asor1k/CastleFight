using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight {
    public class CastleBehaviour : MonoBehaviour
    {
        public Castle castle;
        private UserController user;
        private GoldManager goldManager;
        private void Start()
        {
            user = FindObjectOfType<UserController>();
            goldManager = user.GetComponent<GoldManager>();
            if(gameObject.layer==(int)Team.Team1)
            StartCoroutine(StartMoneyGain());
           
        }
    
        IEnumerator StartMoneyGain()
        {
            yield return new WaitForSeconds(castle.Config.GoldDelay);
            StartCoroutine(StartMoneyGain());
            goldManager.MakeGoldChange(castle.Config.GoldIncome);
        }
    }
}