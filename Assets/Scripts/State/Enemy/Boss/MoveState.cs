using UnityEngine;
using LMS.State;

namespace LMS.Enemy.Boss
{
    public class MoveState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetBossMonsterStateCache("Dead");
            if (obj.IsAttackAble) return StateCache.TryGetBossMonsterStateCache("Attack");
            if (obj.IsChaseAble) return StateCache.TryGetBossMonsterStateCache("Move");
            return StateCache.TryGetBossMonsterStateCache("Idle");
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
