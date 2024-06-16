using LMS.State;

namespace LMS.User
{
    public class DeadState : IState<Player>
    {
        public IState<Player> TransState(Player obj)
        {
            if (obj.Hp > 0) return StateCache.TryGetPlayerStateCache("Idle");
            return StateCache.TryGetPlayerStateCache("Dead");
        }
        public void Enter(Player obj)
        {
            obj.Dead();
            obj.SetAnimation(PlayerInfo.deadAnimName);
        }
        public void Action(Player obj)
        {
        }
        public void Exit(Player obj)
        {
        }
    }
}