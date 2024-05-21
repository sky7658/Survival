using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Controller
{
    public class MouseHandler : InputBase
    {
        public bool isClick => Input.GetMouseButton(0);
        public Vector2 clickPos => Input.mousePosition;
        public float wheelMount => Input.GetAxis("Mouse ScrollWheel");
        public float x
        {
            get
            {
                if (Time.timeScale == 0f) return 0f;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) return -1;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) return 1;
                else return 0;
            }
        }
        public float y
        {
            get
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) return -1;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) return 1;
                else return 0;
            }
        }
    }

}
