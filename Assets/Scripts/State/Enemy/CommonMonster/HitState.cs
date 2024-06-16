using LMS.Controller;
using LMS.State;

namespace LMS.Enemy.Common
{
    public class HitState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsHit) return null;
            if (obj.IsAttackAble && !obj.IsHit) return null;
            if (obj.IsChaseAble && !obj.IsHit) return null;
            return null;
        }
        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonHitAnimName);
            obj.KnockBack();
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}