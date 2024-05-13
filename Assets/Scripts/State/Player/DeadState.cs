using LMS.Enemy;
using LMS.State;
using UnityEngine;

namespace LMS.User
{
    public class DeadState : IState<Player>
    {
        public bool Idle(Player obj) => obj.Hp > 0;
        public bool Move(Player obj) => false;
        public bool Attack(Player obj) => false;
        public bool Hit(Player obj) => false;
        public bool Dead(Player obj) => obj.Hp <= 0;

        public void Enter(Player obj)
        {
            obj.SetAnimation(PlayerInfo.deadAnimName);
            obj.Dead();
        }
        public void Action(Player obj)
        {
        }
        public void Exit(Player obj)
        {
            obj.SetOriginColor();
        }
    }
}