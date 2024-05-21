using LMS.State;
using UnityEngine;

namespace LMS.Enemy.Common
{
    public class HitState : IState<CommonMonster>
    {
        public bool Idle(CommonMonster obj) => true;
        public bool Move(CommonMonster obj) => obj.IsChaseAble && !obj.IsHit;
        public bool Attack(CommonMonster obj) => obj.IsAttackAble && !obj.IsHit;
        public bool Hit(CommonMonster obj) => obj.IsHit;
        public bool Dead(CommonMonster obj) => obj.Hp <= 0;

        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonHitAnimName);
            obj.KnockBack();
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}