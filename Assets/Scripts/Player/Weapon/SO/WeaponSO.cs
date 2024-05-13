using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    [CreateAssetMenu(fileName = "Weapon SO", menuName = "Scriptable Object/Weapon SO", order = int.MaxValue)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] private string objName;
        public string ObjName { get { return objName; } }

        [SerializeField] private float atk;
        public float Atk { get { return atk; } }

        [SerializeField] private int penetrationCnt;
        public int PenetrationCnt { get { return penetrationCnt; } }

        [SerializeField] private float coolTime;
        public float CoolTime { get { return coolTime; } }

        [SerializeField] private float speed;
        public float Speed { get { return speed; } }

        [SerializeField] private int objectCount;
        public int ObjectCount { get { return objectCount; } }
    }
}

