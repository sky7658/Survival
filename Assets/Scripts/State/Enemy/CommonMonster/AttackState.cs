using LMS.State;

namespace LMS.Enemy.Common
{
    public class AttackState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetCommomMonsterStateCache("Dead");
            if (obj.IsHit) return StateCache.TryGetCommomMonsterStateCache("Hit");
            if (obj.IsAttackAble || obj.IsAtk) return StateCache.TryGetCommomMonsterStateCache("Attack");
            if (!obj.IsAtk && obj.IsChaseAble) return StateCache.TryGetCommomMonsterStateCache("Move");
            return StateCache.TryGetCommomMonsterStateCache("Idle");
        }
        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonAttackAnimName);
            obj.Attack();
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}