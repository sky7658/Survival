using UnityEngine;
using LMS.State;
using LMS.General;
using UnityEditor;

namespace LMS.Enemy
{
    public class HitState : IState<Monster>
    {
        public bool Idle(Monster obj) => true;
        public bool Move(Monster obj) => obj.IsChaseAble && !obj.IsHit;
        public bool Attack(Monster obj) => obj.IsAttackAble && !obj.IsHit;
        public bool Hit(Monster obj) => obj.IsHit;
        public bool Dead(Monster obj) => obj.Hp <= 0;

        public void Enter(Monster obj)
        {
            obj.SetAnimation(MonsterInfo.hitAnimName);
            obj.KnockBack();
        }
        public void Action(Monster obj)
        {
        }
        public void Exit(Monster obj)
        {
        }
    }
}