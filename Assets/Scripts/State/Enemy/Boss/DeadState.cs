using LMS.State;

namespace LMS.Enemy.Boss
{
    public class DeadState : IState<BossMonster>
    {
        public bool Idle(BossMonster obj) => true;
        public bool Move(BossMonster obj) => obj.IsChaseAble;
        public bool Attack(BossMonster obj) => obj.IsAttackAble;
        public bool Hit(BossMonster obj) => false;
        public bool Dead(BossMonster obj) => obj.Hp <= 0;

        public void Enter(BossMonster obj)
        {
            obj.SetAnimation(MonsterInfo.bossDeadAnimName);
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}
