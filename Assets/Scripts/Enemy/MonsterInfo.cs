using System.Collections.Generic;
using LMS.Manager;

namespace LMS.Enemy
{
    public static class MonsterInfo
    {
        public static readonly MonsterNameSO mnameSO = ResourceManager.Instance.GetSO<MonsterNameSO>("MonsterNameSO");
        public static readonly int commonMonsterTypeCount = 4;
        public static readonly int bossMonsterTypeCount = 1;

        public static readonly string monsterTag = "Enemy";
        public static readonly string monsterLayer = "Enemy";

        public static readonly string commonIdleAnimName = "IsMove";
        public static readonly string commonMoveAnimName = "IsMove";
        public static readonly string commonAttackAnimName = "Attack";
        public static readonly string commonHitAnimName = "Hit";
        public static readonly string commonDeadAnimName = "Dead";

        public static readonly string bossIdleAnimName = "IsMove";
        public static readonly string bossMoveAnimName = "IsMove";
        public static readonly string bossAttackAnimName = "Attack";
        public static readonly string bossResetAnimName = "Reset";
        public static readonly string bossUpgradeAnimName = "Upgrade";
        public static readonly string bossHitAnimName = "Hit";
        public static readonly string bossDeadAnimName = "Dead";

        public static readonly Dictionary<string, float> monsterAtkRanges = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 1f },
            { mnameSO.Pebble, 0.5f },
            { mnameSO.Slime, 1f }
        };
        public static readonly Dictionary<string, float> mAtkTimes = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 1f },
            { mnameSO.Pebble, 1f },
            { mnameSO.Slime, 1f }
        };

        public static readonly Dictionary<string, float> mCoolTimes = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 1f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f },
            { mnameSO.Golem, 3f }
        };

        public static readonly Dictionary<string, float> knockBackTime = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 0.3f },
            { mnameSO.Crab, 0.3f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f }
        };

        public static readonly Dictionary<string, Dictionary<string, float>> bossAtkTimes = new Dictionary<string, Dictionary<string, float>>()
        {
            { "Golem", new Dictionary<string, float>() { { "Rush", 1f }, { "Punch", 1.2f }, { "MagicAttack", 1.5f }, { "Laser", 1.2f }, { "Defense", 3.5f } } }
        };

        public static readonly Dictionary<string, Dictionary<string, float>> bossAtkRanges = new Dictionary<string, Dictionary<string, float>>()
        {
            { "Golem", new Dictionary<string, float>() { { "Rush", 5f }, { "Punch", 1.5f }, { "MagicAttack", 6f }, { "Laser", 10f }, { "Defense", -1f } } }
        };

        public static readonly string laserName = "Laser";
    }

}
