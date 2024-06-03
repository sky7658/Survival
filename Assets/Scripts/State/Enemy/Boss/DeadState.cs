using LMS.State;

namespace LMS.Enemy.Boss
{
    public class DeadState : IState<BossMonster>
    {
        public bool Idle(BossMonster obj) => obj.Hp > 0;
        public bool Move(BossMonster obj) => false;
        public bool Attack(BossMonster obj) => false;
        public bool Hit(BossMonster obj) => false;
        public bool Dead(BossMonster obj) => obj.Hp <= 0;

        public void Enter(BossMonster obj)
        {
            if (!obj.TransformMode && true /* 특정 조건을 달성하면 2페이즈로 넘어갈 수 있음*/) 
                obj.TransformInit();
            else
            {
                obj.SetAnimation(MonsterInfo.bossDeadAnimName);
                obj.Dead();
            }
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}
