using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.General
{
    public static class EntityInfo
    {
        public static readonly float deadTime = 1f;
        public static readonly float keepHitTime = 0.3f;

        public static readonly string p_IdleAnimName = "IsMove";
        public static readonly string p_MoveAnimName = "IsMove";
        public static readonly string p_DeadAnimName = "Dead";
        public static readonly string p_ReviveAnimName = "Revive";

        public static readonly string m_IdleAnimName = "IsMove";
        public static readonly string m_MoveAnimName = "IsMove";
        public static readonly string m_AttackAnimName = "Attack";
        public static readonly string m_HitAnimName = "Hit";
        public static readonly string m_DeadAnimName = "Dead";
    }

}

