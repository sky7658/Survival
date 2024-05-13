using UnityEngine;
using LMS.State;
using LMS.General;
using Unity.VisualScripting;

namespace LMS.Enemy
{
    public class IdleState : IState<Monster>
    {
        public bool Idle(Monster obj) => true;
        public bool Move(Monster obj) => obj.IsChaseAble;
        public bool Attack(Monster obj) => obj.IsAttackAble;
        public bool Hit(Monster obj) => obj.IsHit;
        public bool Dead(Monster obj) => obj.Hp <= 0;

        public void Enter(Monster obj)
        {
            obj.SetAnimation(EntityInfo.m_IdleAnimName, false);
        }
        public void Action(Monster obj)
        {
        }
        public void Exit(Monster obj)
        {
        }
    }

}
