using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class MainMenuLayout : UILayout
    {
        [SerializeField] private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(PrepareGameSet);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
        }
        
        private void PrepareGameSet()
        {
            EventBusController.I.Bus.Publish(new GameSetReadyEvent());
        }
    }
}