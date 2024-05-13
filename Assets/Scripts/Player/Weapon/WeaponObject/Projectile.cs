using LMS.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public abstract class Projectile : WeaponObject
    {
        private Rigidbody2D rig;
        protected void SetVelocity(Vector2 dir) => rig.velocity = dir * wInfo.speed;
        private Collider2D col;
        public override void Initialized(General.WeaponInfo wInfo, Vector2 pos)
        {
            base.Initialized(wInfo, pos);
        }
        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }
    }
}
