using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Utility
{
    public static class UtilFunctions
    {
        public static List<T> TransArrayToList<T>(T[] values)
        {
            List<T> _values = new List<T>();
            for (int i = 0; i < values.Length; i++) _values.Add(values[i]);
            return _values;
        }

        private static readonly Dictionary<float, WaitForSeconds> timeCache = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            if (!timeCache.TryGetValue(seconds, out var wfs)) timeCache.Add(seconds, wfs = new WaitForSeconds(seconds));
            return wfs;
        }
    }
}