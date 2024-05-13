using LMS.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public class Sword : WeaponObject
    {
        private Transform effectObject;
        private void Awake()
        {
            effectObject = transform.GetChild(0);
        }
        public override void Initialized(WeaponInfo wInfo, Transform target)
        {
            base.Initialized(wInfo, target);
            transform.position = target.position;
            transform.parent = target;
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
