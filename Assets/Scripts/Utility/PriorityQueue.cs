using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Utility
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> array = new List<T>();
        public bool IsEmpty => array.Count == 0;
        public int Length => array.Count;
        public T Top() => array[0];
        public void Pop() => array.RemoveAt(0);
        public void Push(T value)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].CompareTo(value) >= 0)
                {
                    array.Insert(i, value);
                    return;
                }
            }
            array.Add(value);
        }
    }
}