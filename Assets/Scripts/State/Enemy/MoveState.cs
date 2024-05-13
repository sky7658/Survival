using UnityEngine;
using LMS.State;
using LMS.General;

namespace LMS.Enemy
{
    public class MoveState : IState<Monster>
    {
        public bool Idle(Monster obj) => true;
        public bool Move(Monster obj) => obj.IsChaseAble;
        public bool Attack(Monster obj) => obj.IsAttackAble;
        public bool Hit(Monster obj) => obj.IsHit;
        public bool Dead(Monster obj) => obj.Hp <= 0;

        public void Enter(Monster obj)
        {
            obj.SetAnimation(EntityInfo.m_MoveAnimName, true);
        }
        public void Action(Monster obj)
        {
            obj.ChaseToTarget();
        }
        public void Exit(Monster obj)
        {
            obj.Move(Vector2.zero);
        }
    }

}
