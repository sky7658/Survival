using LMS.State;

namespace LMS.Enemy.Boss
{
    public class IdleState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetBossMonsterStateCache("Dead");
            if (obj.IsAttackAble && !obj.IsTrans) return StateCache.TryGetBossMonsterStateCache("Attack");
            if (obj.IsChaseAble && !obj.IsTrans) return StateCache.TryGetBossMonsterStateCache("Move");
            return StateCache.TryGetBossMonsterStateCache("Idle");
        }
        public void Enter(BossMonster obj)
        {
            if (obj.IsTrans && obj.TransformMode) obj.SetAnimation(MonsterInfo.bossUpgradeAnimName);
            else if (obj.IsTrans) obj.SetAnimation(MonsterInfo.bossResetAnimName);
            else obj.SetAnimation(MonsterInfo.bossIdleAnimName, false);
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}
