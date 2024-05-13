using LMS.User;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LMS.Utility
{
    [Serializable]
    public class ObjectInfo
    {
        public string objName;
        public int count;
        public Transform ableParent;
        public Transform disableParent;
        public Component comp;
    }
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        [SerializeField] private List<ObjectInfo> objectInfos = new List<ObjectInfo>();
        private Dictionary<string, object> pools = new Dictionary<string, object>();
        private void OnEnable()
        {
            CreateObjects();
        }

        private void CreateObjects()
        {
            foreach (var objectInfo in objectInfos)
            {
                Type _type = objectInfo.comp.GetType();
                if (!_type.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    Debug.Log($"Component type {_type} is not a MonoBehaviour.");
                    return;
                }
                pools.Add(objectInfo.objName, Activator.CreateInstance(typeof(GenericObjectPool<>).MakeGenericType(_type), objectInfo));
            }
        }

        public T GetObject<T>(string name) where T : MonoBehaviour
        {
            if (pools.TryGetValue(name, out var objectPool) && objectPool is GenericObjectPool<T> typedObjectPool)
            {
                return typedObjectPool.GetObject();
            }
            
            Debug.Log("ObjectInfo에 없는 정보입니다.");
            return default(T);
        }

        public void ReturnObject<T>(T returnObj, string name) where T : MonoBehaviour
        {
            if (pools.TryGetValue(name, out var objectPool) && objectPool is GenericObjectPool<T> typedObjectPool)
            {
                typedObjectPool.ReturnObject(returnObj);
                return;
            }
            Debug.Log("오브젝트 반환 오류");
        }
    }
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        public ObjectInfo info;
        private Queue<T> objectQueue = new Queue<T>();

        public GenericObjectPool(ObjectInfo info)
        {
            this.info = info;
            for (int i = 0; i < info.count; i++) objectQueue.Enqueue(CreateObject());
        }

        private T CreateObject()
        {
            var newObj = GameObject.Instantiate(Manager.ResourceManager.Instance.GetObject<T>(info.objName));
            SetActParent(newObj.gameObject, false);
            return newObj;
        }

        public T GetObject()
        {
            if (objectQueue.Count > 0)
            {
                var obj = objectQueue.Dequeue();
                SetActParent(obj.gameObject, true);
                return obj;
            }
            else
            {
                var newObj = CreateObject();
                SetActParent(newObj.gameObject, true);
                return newObj;
            }
        }

        public void ReturnObject(T returnObj)
        {
            SetActParent(returnObj.gameObject, false);
            objectQueue.Enqueue(returnObj);
        }

        private void SetActParent(GameObject obj, bool active)
        {
            obj.SetActive(active);
            if (active)
            {
                if (info.ableParent != null) obj.transform.SetParent(info.ableParent);
                else obj.transform.SetParent(null);
            }
            else
            {
                if (info.disableParent != null) obj.transform.SetParent(info.disableParent);
                else obj.transform.SetParent(null);
            }
        }
    }
}
