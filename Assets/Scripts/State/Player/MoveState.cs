using UnityEngine;
using LMS.State;
using LMS.Controller;

namespace LMS.User
{
    public class MoveState : IState<Player>
    {
        public IState<Player> TransState(Player obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetPlayerStateCache("Dead");
            if (!InputManager.isMoveKeyDown()) return StateCache.TryGetPlayerStateCache("Idle");
            return StateCache.TryGetPlayerStateCache("Move");
        }
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