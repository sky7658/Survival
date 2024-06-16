using LMS.State;

namespace LMS.Enemy.Common
{
    public class AttackState : IState<CommonMonster>
    {
        public IState<CommonMonster> TransState(CommonMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.IsHit) return null;
            if (obj.IsAttackAble || obj.IsAtk) return null;
            if (!obj.IsAtk && obj.IsChaseAble) return null;
            return null;
        }
        public void Enter(CommonMonster obj)
        {
            obj.SetAnimation(MonsterInfo.commonAttackAnimName);
            obj.Attack();
        }
        public void Action(CommonMonster obj)
        {
        }
        public void Exit(CommonMonster obj)
        {
        }
    }
}