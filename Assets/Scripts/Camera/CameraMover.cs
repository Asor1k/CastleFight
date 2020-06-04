using CastleFight.Core;
using CastleFight.Core.Extensions;
using Core;
using UnityEngine;

namespace CastleFight
{
    public class CameraMover : UserAbility
    {
        [SerializeField] private Transform camTr;
        [SerializeField] private CameraMoverSettings settings;
        
        private Vector3 cursorPos;
        private float xPercentCursorPos;
        private float yPercentCursorPos;

        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        private bool isBlockedRight;
        private bool isBlockedUp;
        private bool isBlockedLeft;
        private bool isBlockedDown;
        private float timer;
        bool isMoving = false;
        bool isBoosting = false;
        float speed;
        private void Update()
        {
            CheckCursorPosition();
            if (isMoving)
            {
                if (timer >= settings.TimeUntillBoost)
                {
                    isBoosting = true;
                }
            }
            else
            {
                timer = 0;
                isBoosting = false;
            }
        }

        
        
        private void CheckForBorders()
        {
            if (camTr.position.z >= maxX) isBlockedRight = true;
            if (camTr.position.z <= minX) isBlockedLeft = true;
            if (camTr.position.x >= maxY) isBlockedDown = true;
            if (camTr.position.x <= minY) isBlockedUp = true;
        }

        private void CheckCursorPosition()
        {
            cursorPos.Set(Input.mousePosition);
            xPercentCursorPos = 1f / Screen.width * cursorPos.x;
            yPercentCursorPos = 1f / Screen.height * cursorPos.y;

            CheckForBorders();
            isMoving = false;
            if (xPercentCursorPos <= settings.ScreenSensibility && !isBlockedLeft)
            {
                isBlockedRight = false;
                isMoving = true;
                MoveCamera(Vector3.left);
            }
            else if (xPercentCursorPos >= 1f - settings.ScreenSensibility && !isBlockedRight)
            {
                isBlockedLeft = false;
                MoveCamera(Vector3.right);
                isMoving = true;
            }

            if (yPercentCursorPos <= settings.ScreenSensibility && !isBlockedDown)
            {
                isBlockedUp = false;
                MoveCamera(Vector3.back);
                isMoving = true;
            }
            else if (yPercentCursorPos >= 1f - settings.ScreenSensibility && !isBlockedUp)
            { 
                isBlockedDown = false;
                MoveCamera(Vector3.forward);
                isMoving = true;
            }
        }

        private void MoveCamera(Vector3 direction)
        {
            timer += Time.deltaTime;
            if (isBoosting)
            {
                speed = settings.BoostedSpeed;
            }
            else
            {
                speed = settings.Speed;
            }
            camTr.Translate(direction * speed * Time.deltaTime);
        }

        public override void Unlock()
        {

        }

        public override void Lock()
        {
        }
    }
}