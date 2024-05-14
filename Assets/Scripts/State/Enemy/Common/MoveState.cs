using UnityEngine;
using LMS.State;

namespace LMS.Enemy.Common
{
    public class MoveState : IState<CommonMonster>
    {
        public bool Idle(CommonMonster obj) => true;
        public bool Move(CommonMonster obj) => obj.IsChaseAble;
        public bool Attack(CommonMonster obj) => obj.IsAttackAble;
        public bool Hit(CommonMonster obj) => obj.IsHit;
        public bool Dead(CommonMonster obj) => obj.Hp <= 0;

        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonMoveAnimName, true);
        }
        public void Action(CommonMonster obj)
        {
            obj.ChaseToTarget();
        }
        public void Exit(CommonMonster obj)
        {
            obj.Move(Vector2.zero);
        }
    }

}
