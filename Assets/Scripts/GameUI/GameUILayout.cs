using CastleFight.Core;
using UnityEngine;

namespace CastleFight.GameUI
{
    public class GameUILayout : UILayout
    {
        [SerializeField] private UILayout[] parts;

        public override void Show()
        {
            base.Show();
            ShowParts();
        }

        public override void Hide()
        {
            base.Hide();
            HideParts();
        }
        
        private void ShowParts()
        {
            foreach (var part in parts)
            {
                part.Show();
            }
        }

        private void HideParts()
        {
            foreach (var part in parts)
            {
                part.Hide();
            }
        }
    }
}