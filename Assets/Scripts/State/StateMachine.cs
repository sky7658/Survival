using System.Collections.Generic;
using UnityEngine;
using LMS.General;
using LMS.User;
using LMS.Enemy;
using Unity.VisualScripting;

namespace LMS.State
{

    public abstract class StateMachine<T> where T : Entity
    {
        private T obj;
        private IState<T> curState;
        protected Dictionary<string, IState<T>> statecache = new Dictionary<string, IState<T>>();

        public StateMachine(T refObj)
        {
            obj = refObj;
        }

        public virtual void Initailized()
        {
            curState = statecache["Idle"];
        }

        protected void ChangeState(IState<T> newState)
        { 
            if (newState.Equals(null) || curState.Equals(newState)) return;
            curState?.Exit(obj);
            curState = newState;
            curState?.Enter(obj);
        }

        private IState<T> CheckTransState()
        {
            IState<T> _state = null;
            if (curState.Dead(obj)) if (statecache.TryGetValue("Dead", out _state)) return _state;
            if (curState.Hit(obj)) if (statecache.TryGetValue("Hit", out _state)) return _state;
            if (curState.Attack(obj)) if (statecache.TryGetValue("Attack", out _state)) return _state;
            if (curState.Move(obj)) if (statecache.TryGetValue("Move", out _state)) return _state;
            if (curState.Idle(obj)) if (statecache.TryGetValue("Idle", out _state)) return _state;

            Debug.Log("CheckTransState is Null");
            return _state;
        }

        public void UpdateState()
        {
            ChangeState(CheckTransState());
            curState.Action(obj);
        }
    }

    public class PlayerStateMachine : StateMachine<Player>
    {
        public PlayerStateMachine(Player refObj) : base(refObj)
        {
            statecache.Add("Idle", new User.IdleState());
            statecache.Add("Move", new User.MoveState());
            statecache.Add("Dead", new User.DeadState());
            Initailized();
        }

        public override void Initailized()
        {
            base.Initailized();
        }
    }

    public class MonsterStateMachine : StateMachine<Monster>
    {
        public MonsterStateMachine(Monster refObj) : base(refObj)
        {
            statecache.Add("Idle", new Enemy.IdleState());
            statecache.Add("Move", new Enemy.MoveState());
            statecache.Add("Attack", new AttackState());
            statecache.Add("Hit", new HitState());
            statecache.Add("Dead", new Enemy.DeadState());
            Initailized();
        }

        public override void Initailized()
        {
            base.Initailized();
        }
    }
}