using LMS.Controller;
using LMS.State;

namespace LMS.User
{
    public class IdleState : IState<Player>
    {
        public IState<Player> TransState(Player obj)
        {
            if (obj.Hp <= 0) return StateCache.TryGetPlayerStateCache("Dead");
            if (InputManager.isMoveKeyDown()) return StateCache.TryGetPlayerStateCache("Move");
            return StateCache.TryGetPlayerStateCache("Idle");
        }
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
