using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight.GameUI
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private void Start()
        {
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            EventBusController.I.Bus.Publish(new ExitToMainMenuEvent());
        }
    }
}