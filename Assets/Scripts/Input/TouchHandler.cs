using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Controller
{
    public class TouchHandler : InputBase
    {
        public bool isClick => Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began);
        public Vector2 clickPos => Input.GetTouch(0).position;
        public float wheelMount => Input.GetAxis("Mouse ScrollWheel");
        private UI.VirtualJoyStick joyStick;
        public TouchHandler()
        {
            
        }
        public float x
        {
            get
            {
                return 0;
            }
        }
        public float y
        {
            get
            {
                return 0;
            }
        }
    }
}
