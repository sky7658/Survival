using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LMS.Controller
{
    public class InputManager
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private static InputBase m_InputHandler = new TouchHandler();
#else 
        private static InputBase m_InputHandler = new MouseHandler();
#endif
        public static bool isClick => m_InputHandler.isClick;
        public static bool isMoveKeyDown()
        {
            if (m_InputHandler.x != 0 || m_InputHandler.y != 0) return true;
            return false;
        }
        public static bool isMoveRightKey => m_InputHandler.x > 0;
        public static bool isMoveX => m_InputHandler.x != 0;
        public static bool isMoveVectorX => moveVector.x != 0;
        public static bool isMoveUpKey => m_InputHandler.y > 0;
        public static bool isMoveY => m_InputHandler.y != 0;
        public static bool isMoveVectorY => moveVector.y != 0;
        public static Vector2 moveVector = new Vector2(1, 0);
        public static Vector2 GetMoveVector()
        {
            var v = new Vector2(m_InputHandler.x, m_InputHandler.y).normalized;
            moveVector = v != Vector2.zero ? v : moveVector;
            return moveVector;
        }
    }
}
