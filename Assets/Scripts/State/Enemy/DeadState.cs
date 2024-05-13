using UnityEngine;
using LMS.State;
using LMS.General;

namespace LMS.Enemy
{
    public class DeadState : IState<Monster>
    {
        public bool Idle(Monster obj) => false;
        public bool Move(Monster obj) => false;
        public bool Attack(Monster obj) => false;
        public bool Hit(Monster obj) => false;
        public bool Dead(Monster obj) => obj.Hp <= 0;

        public void Enter(Monster obj)
        {
            obj.SetAnimation(EntityInfo.m_DeadAnimName);
            obj.Dead();
        }
        public void Action(Monster obj)
        {
        }
        public void Exit(Monster obj)
        {
        }
    }

}
