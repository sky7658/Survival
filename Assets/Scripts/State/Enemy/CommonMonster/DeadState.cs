using LMS.State;

namespace LMS.Enemy.Common
{
    public class DeadState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return null;
            return null;
        }
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