using LMS.Manager;
using System;
using UnityEngine;

namespace LMS.UI
{
    public static class ButtonEvent
    {
        public static void UIExitEvent(GameObject target) => ButtonClickEvent(() => target.SetActive(false));
        public static void GameExitEvent()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public static void ButtonClickEvent(Action action)
        {
            SoundManager.Instance.PlaySFX("ButtonClick");
            action();
        }
    }
}
