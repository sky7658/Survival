using LMS.State;

namespace LMS.Enemy.Boss
{
    public class IdleState : IState<BossMonster>
    {
        public bool Idle(BossMonster obj) => true;
        public bool Move(BossMonster obj) => obj.IsChaseAble;
        public bool Attack(BossMonster obj) => obj.IsAttackAble;
        public bool Hit(BossMonster obj) => false;
        public bool Dead(BossMonster obj) => obj.Hp <= 0;

        public void Enter(BossMonster obj)
        {
            if (obj.IsTransition) obj.SetAnimation(MonsterInfo.bossResetAnimName);
            else obj.SetAnimation(MonsterInfo.bossIdleAnimName, false);
        }
        public void Action(BossMonster obj)
        {
            obj.FlipX();
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}
