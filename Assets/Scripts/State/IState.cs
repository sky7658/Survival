using System.Collections.Generic;
using UnityEngine;
using LMS.General;

namespace LMS.State
{
    public interface IState<T> where T : Entity
    {
        public IState<T> TransState(T obj);
        public void Enter(T obj);
        public void Action(T obj);
        public void Exit(T obj);
    }

    public class StateCache
    {
        private static Dictionary<string, IState<User.Player>> pStateCache = new Dictionary<string, IState<User.Player>>()
        {
            { "Idle", new User.IdleState() },
            { "Move", new User.MoveState() },
            { "Dead", new User.DeadState() }
        };

        private static Dictionary<string, IState<Enemy.CommonMonster>> cmStateCache = new Dictionary<string, IState<Enemy.CommonMonster>>()
        {
            { "Idle", new Enemy.Common.IdleState() },
            { "Move", new Enemy.Common.MoveState() },
            { "Attack", new Enemy.Common.AttackState() },
            { "Hit", new Enemy.Common.HitState() },
            { "Dead", new Enemy.Common.DeadState() }
        };

        private static Dictionary<string, IState<Enemy.BossMonster>> bmStateCache = new Dictionary<string, IState<Enemy.BossMonster>>()
        {
            { "Idle", new Enemy.Boss.IdleState() },
            { "Move", new Enemy.Boss.MoveState() },
            { "Attack", new Enemy.Boss.AttackState() },
            { "Dead", new Enemy.Boss.DeadState() }
        };


        public static IState<User.Player> TryGetPlayerStateCache(string stateName)
        {
            if (!pStateCache.TryGetValue(stateName, out var _state))
            {
                Debug.Log($"{stateName} is not exist in PlayerState");
                return null;
            }
            return _state;
        }

        public static IState<Enemy.CommonMonster> TryGetCommomMonsterStateCache(string stateName)
        {
            if (!cmStateCache.TryGetValue(stateName, out var _state)) 
            {
                Debug.Log($"{stateName} is not exist in CommonMonsterState");
                return null;
            }
            return _state;
        }

        public static IState<Enemy.BossMonster> TryGetBossMonsterStateCache(string stateName)
        {
            if (!bmStateCache.TryGetValue(stateName, out var _state))
            {
                Debug.Log($"{stateName} is not exist in BossMonsterState");
                return null;
            }
            return _state;
        }
    }
}
