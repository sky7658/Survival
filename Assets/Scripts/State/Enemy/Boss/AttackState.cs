using LMS.State;

namespace LMS.Enemy.Boss
{
    public class AttackState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsAttackAble || obj.IsAtk) return null;
            if (obj.IsChaseAble && !obj.IsAtk && !obj.SpecialAttackMode) return null;
            return null;
        }
        public void Enter(BossMonster obj)
        {
            obj.SetAnimation(MonsterInfo.bossAttackAnimName);
            obj.Attack();
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}