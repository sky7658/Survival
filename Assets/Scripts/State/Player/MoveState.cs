using UnityEngine;
using LMS.State;
using LMS.Controller;

namespace LMS.User
{
    public class MoveState : IState<Player>
    {
        public bool Idle(Player obj) => !InputManager.isMoveKeyDown();
        public bool Move(Player obj) => InputManager.isMoveKeyDown();
        public bool Attack(Player obj) => InputManager.isClick;
        public bool Hit(Player obj) => false;
        public bool Dead(Player obj) => obj.Hp <= 0;


        public void Enter(Player obj)
        {
            obj.SetAnimation(PlayerInfo.moveAnimName, true);
        }
        public void Action(Player obj)
        {
            if (InputManager.isMoveX) obj.FlipX(!InputManager.isMoveRightKey);
            obj.Move(InputManager.GetMoveVector());
        }
        public void Exit(Player obj)
        {
            obj.Move(Vector2.zero);
        }
    }
}