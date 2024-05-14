using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public class Golem : BossMonster
    {
        public override void SetAnimation(string animName)
        {
            var newAnimName = animName;

            if (animName.Equals("Attack"))
            {
                if (atkDelegate.Method.Name.Equals("Rush") || atkDelegate.Method.Name.Equals("MagicAttack")) newAnimName += "1";
                else if (atkDelegate.Method.Name.Equals("Punch")) newAnimName += "2";
            }
            else if (animName.Equals("Hit") || animName.Equals("Dead"))
            {
                if (IsTransition) newAnimName += "A";
                else newAnimName += "B";
            }
            base.SetAnimation(newAnimName);
        }
        public override void Initialized()
        {
            base.Initialized();
            atkTypes = new GolemAttackType();
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }

}
