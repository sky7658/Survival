using LMS.State;

namespace LMS.Enemy.Common
{
    public class IdleState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsHit) return null;
            if (obj.IsAttackAble) return null;
            if (obj.IsChaseAble) return null;
            return null;
        }
        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonIdleAnimName, false);
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}