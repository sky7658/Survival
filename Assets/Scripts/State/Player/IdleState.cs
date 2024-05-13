using UnityEngine;
using LMS.State;
using LMS.Controller;

namespace LMS.User
{
    public class IdleState : IState<Player>
    {
        public bool Idle(Player obj) => !InputManager.isMoveKeyDown();
        public bool Move(Player obj) => InputManager.isMoveKeyDown();
        public bool Attack(Player obj) => Input.GetMouseButton(0);
        public bool Hit(Player obj) => false;
        public bool Dead(Player obj) => obj.Hp <= 0;

        public void Enter(Player obj)
        {
            obj.SetAnimation(PlayerInfo.idleAnimName, false);
        }
        public void Action(Player obj)
        {
        }
        public void Exit(Player obj)
        {
        }
    }
}
