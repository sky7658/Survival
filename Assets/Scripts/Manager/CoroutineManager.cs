using System.Collections;
using UnityEngine;

namespace LMS.Manager
{
    public class CoroutineManager : Utility.MonoSingleton<CoroutineManager>
    {
        protected override bool UseDontDestroy() => true;

        public Coroutine ExecuteCoroutine(IEnumerator enumerator) => StartCoroutine(enumerator);
        public void QuitCoroutine(Coroutine routine) => StopCoroutine(routine);
        public void QuitAllCoroutine() => StopAllCoroutines();
    }
}
