using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;
using UnityEditor;

namespace LMS.Manager
{
    public static class ResourceManager
    {

        private static Dictionary<string, ScriptableObject> scriptableObjects;
            
        private static Dictionary<string, GameObject> prefabs;

        private static Dictionary<string, Sprite> sprites;

        private static Dictionary<string, AudioClip> bgmClips;
        private static Dictionary<string, AudioClip> sfxClips;

        static ResourceManager()
        {
            Debug.Log("Called ResourceManager");
            InitResources();
        }
        private static void InitResources()
        {
            scriptableObjects = LoadResources<ScriptableObject>("Scriptable Object");
            prefabs = LoadResources<GameObject>("Prefabs");

            sprites = LoadResources<Sprite>("Images/Weapon");
            bgmClips = LoadResources<AudioClip>("Sound/BGM");
            sfxClips = LoadResources<AudioClip>("Sound/SFX");
        }
        private static Dictionary<string, T> LoadResources<T>(string filePath) where T : Object
        {
            var _resources = Resources.LoadAll<T>(filePath);

            Dictionary<string, T> _newResources = new Dictionary<string, T>();
            foreach (var _resource in _resources) _newResources.Add(_resource.name, _resource);

            return _newResources;
        }
        public static T GetSO<T>(string soName) where T : ScriptableObject
        {
            if (scriptableObjects is null)
            {
                Debug.Log("scriptableObjects is null");
                return null;
            }
            if (!scriptableObjects.TryGetValue(soName, out var _so))
            {
                Debug.Log($"{soName} is Not exist in ScriptableObjects");
                return null;
            }
            return (T)_so;
        }

        public static T GetObject<T>(string objName) where T : MonoBehaviour
        {
            if (prefabs is null)
            {
                Debug.Log("Prefabs is null");
                return null;
            }
            if (!prefabs.TryGetValue(objName, out var _obj))
            {
                Debug.Log($"{objName} is Not exist in Prefabs");
                return null;
            }
            return _obj.GetComponent<T>();
        }

        public static Sprite GetSprite(string sprName)
        {
            if (sprites is null)
            {
                Debug.Log("sprites is null");
                return null;
            }
            if (!sprites.TryGetValue(sprName, out var _spr))
            {
                Debug.Log($"{sprName} is not exist in Sprites");
                return null;
            }
            return _spr;
        }
        public static AudioClip GetClip(string dirName, string clipName)
        {
            Dictionary<string, AudioClip> _cpyClips;

            switch (dirName)
            {
                case "BGM":
                    if (bgmClips is null)
                    {
                        Debug.Log("bgmClips is null");
                        return null;
                    }
                    _cpyClips = bgmClips;
                    break;
                case "SFX":
                    if (sfxClips is null)
                    {
                        Debug.Log("sfxClips is null");
                        return null;
                    }
                    _cpyClips = sfxClips;
                    break;
                default:
                    Debug.Log($"{dirName} is not exist in Sound Directory File names");
                    return null;
            }

            if (!_cpyClips.TryGetValue(clipName, out var _clip))
            {
                Debug.Log($"{clipName} is not exist in {dirName} Directory File");
                return null;
            }

            return _clip;
        }
    }
}