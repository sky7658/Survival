using LMS.General;
using LMS.Manager;

namespace LMS.User
{
    public static class PlayerInfo
    {
        public static readonly EntitySO playerSO = ResourceManager.GetSO<EntitySO>("PlayerSO");
        public static readonly string playerTag = "Player";
        public static readonly string playerLayer = "Player";

        public static readonly string idleAnimName = "IsMove";
        public static readonly string moveAnimName = "IsMove";
        public static readonly string deadAnimName = "Dead";
        public static readonly string reviveAnimName = "Revive";

        public static readonly string[] abilityNames = { "Shoes", "Hat", "Cadny" };
    }
}
