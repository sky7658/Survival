using LMS.Manager;
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
            if (!obj.TransformMode && (GameManager.Instance.ClearCount % 3).Equals(0))
            {
                obj.SetAnimation(MonsterInfo.bossIdleAnimName, false);
                obj.TransformInit();
            }
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
