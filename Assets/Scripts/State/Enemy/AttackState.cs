using UnityEngine;
using LMS.State;
using LMS.General;

namespace LMS.Enemy
{
    public class AttackState : IState<Monster>
    {
        public bool Idle(Monster obj) => true;
        public bool Move(Monster obj) => !obj.IsAtk && obj.IsChaseAble;
        public bool Attack(Monster obj) => obj.IsAttackAble || obj.IsAtk;
        public bool Hit(Monster obj) => obj.IsHit;
        public bool Dead(Monster obj) => obj.Hp <= 0;

        public void Enter(Monster obj)
        {
            obj.SetAnimation(EntityInfo.m_AttackAnimName);
        }
        public void Action(Monster obj)
        {
        }
        public void Exit(Monster obj)
        {
        }
    }
}
