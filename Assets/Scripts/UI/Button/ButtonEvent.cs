using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public static class ButtonEvent
    {
        public static void UIExitEvent(GameObject target) => target.SetActive(false);
        public static void GameExitEvent()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
