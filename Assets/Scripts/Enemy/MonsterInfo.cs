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

        public static readonly float monsterAtkRange = 1f;
        public static readonly Dictionary<string, float> mCoolTimes = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 0.2f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f }
        };

        public static readonly Dictionary<string, float> knockBackTime = new Dictionary<string, float>()
        {
            { mnameSO.Bat, 1f },
            { mnameSO.Crab, 0.2f },
            { mnameSO.Pebble, 0.2f },
            { mnameSO.Slime, 0.2f }
        };
    }

}