using LMS.State;
using UnityEngine;

namespace LMS.Enemy.Common
{
    public class MoveState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetCommomMonsterStateCache("Dead");
            if (obj.IsHit) return StateCache.TryGetCommomMonsterStateCache("Hit");
            if (obj.IsAttackAble) return StateCache.TryGetCommomMonsterStateCache("Attack");
            if (obj.IsChaseAble) return StateCache.TryGetCommomMonsterStateCache("Move");
            return StateCache.TryGetCommomMonsterStateCache("Idle");
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