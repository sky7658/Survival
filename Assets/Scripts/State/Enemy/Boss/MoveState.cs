using UnityEngine;
using LMS.State;

namespace LMS.Enemy.Boss
{
    public class MoveState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsAttackAble) return null;
            if (obj.IsChaseAble) return null;
            return null;
        }
        public void Enter(BossMonster obj)
        {
            obj.SetAnimation(MonsterInfo.bossMoveAnimName, true);
        }
        public void Action(BossMonster obj)
        {
            obj.ChaseToTarget();
        }
        public void Exit(BossMonster obj)
        {
            obj.Move(Vector2.zero);
        }
    }
}
