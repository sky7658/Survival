using LMS.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public static class MonsterInfo
    {
        public static readonly MonsterNameSO mnameSO = ResourceManager.Instance.GetSO<MonsterNameSO>("MonsterNameSO");

        public static readonly string monsterTag = "Enemy";
        public static readonly string monsterLayer = "Enemy";

        public static readonly string idleAnimName = "IsMove";
        public static readonly string moveAnimName = "IsMove";
        public static readonly string attackAnimName = "Attack";
        public static readonly string hitAnimName = "Hit";
        public static readonly string deadAnimName = "Dead";

        public static readonly float monsterAtkRange = 1f;
        public static readonly Dictionary<string, float> mAtkTimes = new Dictionary<string, float>()
        {
            { "Bat", 1f },
            { "Crab", 1f },
            { "Pebble", 1f },
            { "Slime", 1f }
        };

        public static readonly Dictionary<string, float> mCoolTimes = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 1f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f }
        };

        public static readonly Dictionary<string, float> knockBackTime = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 0.3f },
            { mnameSO.Crab, 0.3f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f }
        };
    }

}