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
            if (coroutines.TryGetValue(name, out var coroutine)) coroutines[name] = UtilCoroutine.ExecuteCoroutine(startCor, coroutine);
            else Debug.Log($"{name} is not exist in index of coroutines");
        }
        public void OffCoroutine(string name)
        {
            if (coroutines.TryGetValue(name, out var _coroutine))
            {
                UtilCoroutine.OffCoroutine(ref _coroutine);
                coroutines[name] = _coroutine;
            }
            else Debug.Log($"{name} is not exist in index of coroutines");
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
            List<string> strings = new List<string>(coroutines.Keys);
            foreach (var keyname in strings)
            {
                if (coroutines.TryGetValue(keyname, out var _coroutine))
                {
                    OffCoroutine(ref _coroutine);
                    coroutines[keyname] = _coroutine;
                }
            }
        }
    }
}

