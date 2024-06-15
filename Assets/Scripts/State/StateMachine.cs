using System.Collections.Generic;
using UnityEngine;
using LMS.General;
using LMS.User;
using LMS.Enemy;

namespace LMS.State
{
    public abstract class StateMachine<T> where T : Entity
    {
        private T obj;
        private IState<T> curState;
        protected abstract bool GetStateCache(string stateName, out IState<T> value);
        public StateMachine(T refObj)
        {
            obj = refObj;
        }

        public void Initailized()
        {
            if (!GetStateCache("Idle", out var initState)) return;
            curState = initState;
            curState.Enter(obj);
        }

        public void ChangeState()
        {
            var newState = CheckTransState();
            if (newState == null) return;
            if (curState == null)
            {
                curState = newState;
                curState?.Enter(obj);
                return;
            }
            if (curState.Equals(newState)) return;

            curState?.Exit(obj);
            curState = newState;
            curState?.Enter(obj);
        }

        private IState<T> CheckTransState()
        {
            IState<T> _state = null;

            if (Time.timeScale == 0f) return curState;
            if (curState == null || !Manager.PlayManager.Instance.IsGamePlay) if (GetStateCache("Idle", out _state)) return _state;

            if (curState.Dead(obj)) if (GetStateCache("Dead", out _state)) return _state;
            if (curState.Hit(obj)) if (GetStateCache("Hit", out _state)) return _state;
            if (curState.Attack(obj)) if (GetStateCache("Attack", out _state)) return _state;
            if (curState.Move(obj)) if (GetStateCache("Move", out _state)) return _state;
            if (curState.Idle(obj)) if (GetStateCache("Idle", out _state)) return _state;

            Debug.Log("CheckTransState is Null");
            return _state;
        }

        public void UpdateState()
        {
            curState.Action(obj);
        }
    }

    public class PlayerStateMachine : StateMachine<Player>
    {
        private Dictionary<string, IState<Player>> pStateCache = new Dictionary<string, IState<Player>>()
        {
            { "Idle", new User.IdleState() },
            { "Move", new User.MoveState() },
            { "Dead", new User.DeadState() }
        };
        public PlayerStateMachine(Player refObj) : base(refObj)
        {
            Initailized();
        }
        protected override bool GetStateCache(string stateName, out IState<Player> value)
        {
            if (!pStateCache.TryGetValue(stateName, out var _state))
            {
                Debug.Log($"{stateName} is not exist in PlayerState");
                value = null;
                return false;
            }
            value = _state;
            return true;
        }
    }

    public class MonsterStateMachine : StateMachine<CommonMonster>
    {
        private static Dictionary<string, IState<CommonMonster>> mStateCache = new Dictionary<string, IState<CommonMonster>>()
        {
            { "Idle", new Enemy.Common.IdleState() },
            { "Move", new Enemy.Common.MoveState() },
            { "Attack", new Enemy.Common.AttackState() },
            { "Hit", new Enemy.Common.HitState() },
            { "Dead", new Enemy.Common.DeadState() }
        };
        public MonsterStateMachine(CommonMonster refObj) : base(refObj)
        {
            Initailized();
        }
        protected override bool GetStateCache(string stateName, out IState<CommonMonster> value)
        {
            if (!mStateCache.TryGetValue(stateName, out var _state))
            {
                Debug.Log($"{stateName} is not exist in CommonMonster");
                value = null;
                return false;
            }
            value = _state; 
            return true;
        }
    }
    public class BossStateMachine : StateMachine<BossMonster>
    {
        private Dictionary<string, IState<BossMonster>> mStateCache = new Dictionary<string, IState<BossMonster>>()
        {
            { "Idle", new Enemy.Boss.IdleState() },
            { "Move", new Enemy.Boss.MoveState() },
            { "Attack", new Enemy.Boss.AttackState() },
            { "Dead", new Enemy.Boss.DeadState() }
        };
        public BossStateMachine(BossMonster refObj) : base(refObj)
        {
            Initailized();
        }
        protected override bool GetStateCache(string stateName, out IState<BossMonster> value)
        {
            if (!mStateCache.TryGetValue(stateName, out var _state))
            {
                Debug.Log($"{stateName} is not exist in BossMonsterState");
                value = null;
                return false;
            }
            value = _state;
            return true;
        }
    }
}