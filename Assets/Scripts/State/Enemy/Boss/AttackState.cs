using LMS.State;
using System;

namespace LMS.Enemy.Boss
{
    public class AttackState : IState<BossMonster>
    {
        public bool Idle(BossMonster obj) => true;
        public bool Move(BossMonster obj) => obj.IsChaseAble;
        public bool Attack(BossMonster obj) => obj.IsAttackAble && obj.IsAtk;
        public bool Hit(BossMonster obj) => false;
        public bool Dead(BossMonster obj) => obj.Hp <= 0;

        public void Enter(BossMonster obj)
        {
            obj.SetAnimation(MonsterInfo.bossAttackAnimName);
            obj.Attack();
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}
