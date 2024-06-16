using LMS.State;
using UnityEngine;

namespace LMS.Enemy.Common
{
    public class MoveState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsHit) return null;
            if (obj.IsAttackAble) return null;
            if (obj.IsChaseAble) return null;
            return null;
        }
        public void Enter(CommonMonster obj)
        {
            obj.SetBodyType(RigidbodyType2D.Dynamic);
            obj.SetAnimation(MonsterInfo.commonMoveAnimName, true);
        }
        public void Action(CommonMonster obj)
        {
            obj.ChaseToTarget();
        }
        public void Exit(CommonMonster obj)
        {
            obj.SetBodyType(RigidbodyType2D.Kinematic);
            obj.Move(Vector2.zero);
        }
    }
}