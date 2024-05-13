using LMS.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Utility
{
    public class CoroutineController
    {
        private Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

        public void AddCoroutine(string name)
        {
            if (!coroutines.ContainsKey(name))
            {
                coroutines.Add(name, null);
            }
        }

        public void ExecuteCoroutine(in IEnumerator startCor, string name)
        {
            if (!coroutines.TryGetValue(name, out var coroutine)) Debug.Log("존재하지 않은 코루틴");
            coroutine = UtilCoroutine.ExecuteCoroutine(startCor, coroutine);
        }

        public void OffCoroutine(string name)
        {
            if (!coroutines.TryGetValue(name, out var corutine)) Debug.Log("존재하지 않은 코루틴");
            UtilCoroutine.OffCoroutine(ref corutine);
        }

        public void OffAllCoroutines() => UtilCoroutine.OffAllCoroutines(ref coroutines);
    }

    public static class UtilCoroutine
    {
        public static Coroutine ExecuteCoroutine(in IEnumerator startCor, Coroutine endCor)
        {
            if (endCor != null) OffCoroutine(ref endCor);
            return CoroutineManager.Instance.ExecuteCoroutine(startCor);
        }

        public static void OffCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                CoroutineManager.Instance.QuitCoroutine(coroutine);
                coroutine = null;
            }
        }

        public static void OffAllCoroutines(ref List<Coroutine> coroutines)
        {
            coroutines.ForEach(coroutine => OffCoroutine(ref coroutine));
        }

        public static void OffAllCoroutines(ref Dictionary<string, Coroutine> coroutines)
        {
            if (coroutines == null || coroutines.Count == 0) return;
            foreach (var keyname in coroutines.Keys)
            {
                if (coroutines.TryGetValue(keyname, out var coroutine))
                {
                    OffCoroutine(ref coroutine);
                }
            }
        }
    }
}

