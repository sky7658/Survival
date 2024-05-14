using LMS.State;

namespace LMS.Enemy.Common
{
    public class DeadState : IState<CommonMonster>
    {
        public bool Idle(CommonMonster obj) => false;
        public bool Move(CommonMonster obj) => false;
        public bool Attack(CommonMonster obj) => false;
        public bool Hit(CommonMonster obj) => false;
        public bool Dead(CommonMonster obj) => obj.Hp <= 0;

        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonDeadAnimName);
            obj.Dead();
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }

}
