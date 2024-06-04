using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public static class ButtonEvent
    {
        public static void UIExitEvent(GameObject target) => target.SetActive(false);
    }
}
