using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CastleFight.Core;

namespace CastleFight.MainMenu
{
    public class DropDownManager : UILayout //Added [Asor1k]
    {

        private Dropdown menuDropdownMain;
        [SerializeField] private GameSetProvider setProvider;
        [SerializeField] private List<Toggle> menuToggles;
        [SerializeField] ScrollRect dropList;

        public void Start()
        {
            menuDropdownMain = GetComponent<Dropdown>();
        }
        public void ChangeMenuButton()
        {
            foreach (Dropdown dr in GetComponentsInChildren<Dropdown>())
            {
                if (dr == menuDropdownMain) continue;
                menuDropdownMain = dr;
            }
        }
        public void OnDropdownClick()
        {
            dropList = transform.GetComponentInChildren<ScrollRect>();
            foreach (Toggle tog in dropList.GetComponentsInChildren<Toggle>())
            {
                if (tog.isOn) tog.SetIsOnWithoutNotify(false);
                menuToggles.Add(tog); 
                tog.onValueChanged.AddListener(AddButtonToDrwn);
            }
        }
        private void AddButtonToDrwn(bool b)
        {
            int togNum = -1;
            for (int i = 0; i < menuToggles.Count; i++)
            {
                if (menuToggles[i].isOn)
                {
                    togNum = i;
                }
            }
            menuToggles.Clear();
            switch (togNum)
            {
                case -1:
                    return;
                case 0:
                    setProvider.ExitGame();
                    break;
                case 1:
                    setProvider.StartNewGame();
                    break;

            }

        }


    }
}