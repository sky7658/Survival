using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;
using TMPro;

namespace LMS.Manager
{
    public class ResourceManager : GeneralSingleton<ResourceManager>
    {
        private ScriptableObject[] scriptableObjects = Resources.LoadAll<ScriptableObject>("Scriptable Object");
        private GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
        private Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Weapon");

        public T GetSO<T>(string soName) where T : ScriptableObject
        {
            foreach (var so in scriptableObjects)
            {
                if (so.name.Equals(soName))
                    return (T)so;
            }
            Debug.Log($"{soName} is Not exist in ScriptableObjects");
            return null;
        }

        public T GetObject<T>(string objName) where T : MonoBehaviour
        {
            foreach (var obj in prefabs)
            {
                if (obj.name.Equals(objName))
                    return obj.GetComponent<T>();
            }
            Debug.Log($"{objName} is not exist in Prefabs");
            return null;
        }

        public Sprite GetSprite(string sprName)
        {
            foreach (var obj in sprites)
            {
                if (obj.name.Equals(sprName))
                    return obj;
            }
            Debug.Log($"{sprName} is not exist in Sprites");
            return null;
        }
    }
}