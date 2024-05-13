using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;

namespace LMS.Manager
{
    public class ResourceManager : GeneralSingleton<ResourceManager>
    {
        private ScriptableObject[] scriptableObjects = Resources.LoadAll<ScriptableObject>("Scriptable Object");
        private GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");

        public T GetSO<T>(string soName) where T : ScriptableObject
        {
            foreach (var so in scriptableObjects)
            {
                if (so.name.Equals(soName))
                    return (T)so;
            }
            Debug.Log(soName + " is Not exist to ScriptableObject");
            return null;
        }

        public T GetObject<T>(string objName) where T : MonoBehaviour
        {
            foreach (var obj in prefabs)
            {
                if (obj.name.Equals(objName))
                    return obj.GetComponent<T>();
            }

            return null;
        }
    }
}