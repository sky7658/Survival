using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Manager
{
    public class CoroutineManager : Utility.MonoSingleton<CoroutineManager>
    {
        public Coroutine ExecuteCoroutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }
        public void QuitCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }
    }
}
