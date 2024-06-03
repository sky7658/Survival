using LMS.Controller;
using LMS.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public class Arrow : Projectile
    {
        public override void Initialized(General.WeaponInfo wInfo, Vector2 pos)
        {
            base.Initialized(wInfo, pos);
            var dir = InputManager.moveVector;

            transform.position = pos;

            float axis = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
            transform.rotation = Quaternion.Euler(0, 0, axis);

            SetVelocity(dir);
        }

        protected override void ReturnObject() => Utility.ObjectPool.Instance.ReturnObject(this, GetWoName);
    }
}
