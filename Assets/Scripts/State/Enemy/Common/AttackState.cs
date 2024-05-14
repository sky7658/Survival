using LMS.State;

namespace LMS.Enemy.Common
{
    public class AttackState : IState<CommonMonster>
    {
        public bool Idle(CommonMonster obj) => true;
        public bool Move(CommonMonster obj) => !obj.IsAtk && obj.IsChaseAble;
        public bool Attack(CommonMonster obj) => obj.IsAttackAble && obj.IsAtk;
        public bool Hit(CommonMonster obj) => obj.IsHit;
        public bool Dead(CommonMonster obj) => obj.Hp <= 0;

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
