using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LMS.Enemy
{
    public class Golem : BossMonster
    {
        private bool defenseMode;
        public override void EndAtk()
        {
            base.EndAtk();
            defenseMode = false;
        }
        public override bool IsChaseAble
        {
            get
            {
                bool _flag = Mathf.Abs(TargetPos.y - transform.position.y) < 1f;
                if (atkDelegate.Method.Name.Equals("Rush") || atkDelegate.Method.Name.Equals("Laser"))
                    return !(_flag) || base.IsChaseAble;
                return base.IsChaseAble;
            }
        }
        protected override AttackTypeDelegate<BossMonster> GetAtkType()
        {
            AttackTypeDelegate<BossMonster> newAtk = null;
            specialAtkMode = false;

            while (true) 
            {
                newAtk = base.GetAtkType();
                if (newAtk == null) break;
                if (newAtk.Method.Name.Equals("Rush") || newAtk.Method.Name.Equals("MagicAttack"))
                {
                    if (newAtk.Method.Name.Equals("MagicAttack") && !TransformMode)
                    {
                        specialAtkMode = true;
                        transforming = true;
                    }
                    break;
                }
                else if (!TransformMode && newAtk.Method.Name.Equals("Punch")) break;
                else if (TransformMode && (newAtk.Method.Name.Equals("Laser") || newAtk.Method.Name.Equals("Defense"))) break;
            }
            return newAtk;
        }
        public override void SetAnimation(string animName)
        {
            var newAnimName = animName;

            if (animName.Equals("Attack"))
            {
                if (atkDelegate.Method.Name.Equals("Rush")) newAnimName += "1";
                else if (atkDelegate.Method.Name.Equals("Punch") || atkDelegate.Method.Name.Equals("MagicAttack")) newAnimName += "2";
                else if (atkDelegate.Method.Name.Equals("Laser")) newAnimName += "3";
                else if (atkDelegate.Method.Name.Equals("Defense"))
                {
                    defenseMode = true;
                    newAnimName += "4";
                }
            }
            base.SetAnimation(newAnimName);
        }
        public override void TakeDamage(float value, Vector2 vec = default)
        {
            if (defenseMode) value = 0f;
            base.TakeDamage(value, vec);
        }
        public override void Initialized()
        {
            base.Initialized();
            atkTypes = new GolemAttackType();
            Atk = 1f;
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }

}
