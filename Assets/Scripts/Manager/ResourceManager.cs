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
        private AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        private AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");

        public T GetSO<T>(string soName) where T : ScriptableObject
        {
            if (scriptableObjects is null)
            {
                Debug.Log("scriptableObjects is null");
                return null;
            }
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
            if (prefabs is null)
            {
                Debug.Log("Prefabs is null");
                return null;
            }
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
            if (sprites is null)
            {
                Debug.Log("sprites is null");
                return null;
            }
            foreach (var obj in sprites)
            {
                if (obj.name.Equals(sprName))
                    return obj;
            }
            Debug.Log($"{sprName} is not exist in Sprites");
            return null;
        }

        public AudioClip GetClip(string dirName, string clipName)
        {
            AudioClip[] _clips = null;

            switch (dirName)
            {
                case "BGM":
                    if (bgmClips is null)
                    {
                        Debug.Log("bgmClips is null");
                        return null;
                    }
                    _clips = bgmClips;
                    break;
                case "SFX":
                    if (sfxClips is null)
                    {
                        Debug.Log("sfxClips is null");
                        return null;
                    }
                    _clips = sfxClips;
                    break;
                default:
                    Debug.Log($"{dirName} is not exist in Sound Directory File names");
                    return null;
            }

            foreach (var clip in _clips)
            {
                if (clip.name.Equals(clipName))
                    return clip;
            }

            Debug.Log($"{clipName} is not exist in {dirName} Directory File");
            return null;
        }
    }
}