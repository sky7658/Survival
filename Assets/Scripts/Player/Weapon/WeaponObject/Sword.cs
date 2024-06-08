using LMS.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public class Sword : WeaponObject
    {
        protected static float cumulativeDamage;
        public static float CumulativeDamage { get { return cumulativeDamage; } }
        protected override void IncreaseCumlativeDamage(float value) => cumulativeDamage += value;

        private Transform effectObject;
        private void Awake()
        {
            effectObject = transform.GetChild(0);
        }
        public override void Initialized(WeaponInfo wInfo, Transform target, float atk)
        {
            base.Initialized(wInfo, target, atk);
            transform.position = target.position;
            transform.parent = target;
            IncreaseCumlativeDamage(atk);
        }
        public void ExecuteEffect() => effectObject.gameObject.SetActive(true);
        public void Release()
        {
            effectObject.gameObject.SetActive(false);
            ReturnObject();
        }
        protected override void ReturnObject() => Utility.ObjectPool.Instance.ReturnObject(this, GetWoName);
    }

}
