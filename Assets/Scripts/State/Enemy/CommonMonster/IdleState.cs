using LMS.State;

namespace LMS.Enemy.Common
{
    public class IdleState : IState<CommonMonster>
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
            obj.SetAnimation(MonsterInfo.commonIdleAnimName, false);
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}