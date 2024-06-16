using LMS.State;

namespace LMS.Enemy.Boss
{
    public class IdleState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsAttackAble && !obj.IsTrans) return null;
            if (obj.IsChaseAble && !obj.IsTrans) return null;
            return null;
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
