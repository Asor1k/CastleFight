using CastleFight.Core;
using CastleFight.Core.Extensions;
using Core;
using UnityEngine;

namespace CastleFight
{
    public class CameraMover : UserAbility
    {
        [SerializeField] private Camera Camera;
        [SerializeField] private Transform camTr;
        [SerializeField] private CameraMoverSettings settings;  
        [SerializeField] private float delta;
        [SerializeField] private float minZ;
        [SerializeField] private float maxZ;
        [SerializeField] private float gravity;
        [SerializeField] private float dead;


        protected Plane plane;
        
        private bool isBlockedRight;
        private bool isBlockedLeft;
        private bool isMoving = false;
        private bool canMove = true;
        private float inertZ;

        #region Pevious fields
        private Vector3 cursorPos;
        private float xPercentCursorPos;
        private float yPercentCursorPos;

        [SerializeField] float speed = 60;

        #endregion
        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
            inertZ = 0;
        }
        public void StopMoving()
        {
            canMove = false;
        }
        public void ConinueMoving()
        {
            canMove = true;
        }


        private void Update()
        {
            #region Unity movement
#if UNITY_EDITOR
            if (!canMove) return;

            if (Input.GetMouseButton(0))
            {
                if (!isMoving)
                {
                    isMoving = true;
                    return;
                }
                float x = Input.GetAxis("Mouse X");

                CheckForBorders();

                x = Mathf.Abs(x) > delta ? delta*x/Mathf.Abs(x) : x;
                if(isBlockedLeft && x < 0)
                {
                    x = 0;
                }
                if(isBlockedRight && x > 0)
                {
                    x = 0;
                }
                inertZ = x;
                Vector3 pos = (new Vector3(0, 0, x) * settings.Speed) * Time.deltaTime / Screen.dpi;
                camTr.position -= pos;
            }
            else
            {
                CheckForBorders();
                if (inertZ != 0 && !isBlockedLeft && !isBlockedRight)
                {
                    Vector3 delta2 = new Vector3(0, 0, inertZ);
                    inertZ = inertZ > 0 ? inertZ - Time.deltaTime * gravity : inertZ + Time.deltaTime * gravity;
                    if (Mathf.Abs(inertZ) <= dead)
                    {
                        inertZ = 0;
                    }
                    camTr.transform.Translate(-delta2, Space.World);

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }
#endif
            /*CheckCursorPosition();
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
            }*/
            #endregion

            #region Phone movement
#if UNITY_IOS || UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount >= 1)
            {
                plane.SetNormalAndPosition(transform.up, transform.position);
            }
            Vector3 delta1 = Vector3.zero;

            CheckForBorders();
            if (Input.touchCount >= 1 && canMove)
            {
                delta1 = PlanePositionDelta(Input.GetTouch(0));
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float z = delta1.z;
                    if (isBlockedRight && z < 0)
                    {
                        z = 0;
                    }
                    if (isBlockedLeft && z > 0)
                    {
                        z = 0;
                    }
                    delta1 = new Vector3(0, 0, z);
                    camTr.transform.Translate(delta1, Space.World);
                    inertZ = z;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    inertZ = 0;
                }
            
            }
            else
            {
                if (Input.touchCount == 0 && inertZ !=0 && !isBlockedRight && !isBlockedLeft)
                {
                    delta1 = new Vector3(0, 0, inertZ);

                    inertZ = inertZ > 0 ? inertZ - Time.deltaTime * gravity: inertZ + Time.deltaTime*gravity;
                    if (Mathf.Abs(inertZ) <= dead)
                    {
                        inertZ = 0;
                    }
                    camTr.transform.Translate(delta1, Space.World);

                }
            }
#endif
            #endregion
        }

        protected Vector3 PlanePositionDelta(Touch touch)
        {
            if(touch.phase != TouchPhase.Moved)
            {
                return Vector3.zero;
            }

            Ray rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
            Ray rayNow = Camera.ScreenPointToRay(touch.position);
            if(plane.Raycast(rayBefore, out float enterBefore)&& plane.Raycast(rayNow,out float enterNow))
            {
                return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
            }
            return Vector3.zero;
        }
        protected Vector3 PlanePosition(Vector2 screenPos)
        {
            Ray rayNow = Camera.ScreenPointToRay(screenPos);
            if(plane.Raycast(rayNow, out float enterNow))
            {
                return rayNow.GetPoint(enterNow);
            }
            return Vector3.zero;
        }

         private void CheckForBorders()
        {
            if (camTr.position.z <= minZ)
            {
                isBlockedRight = true;
            }
            else
            {
                isBlockedRight = false;
            }
            if (camTr.position.z >= maxZ)
            {
                isBlockedLeft = true;
            }
            else
            {
                isBlockedLeft = false;
            }
        }

      /*  private void CheckCursorPosition()
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
        */

        public override void Unlock()
        {

        }

        public override void Lock()
        {
        }
    }
}