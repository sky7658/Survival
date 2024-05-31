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
        protected Dictionary<string, IState<T>> statecache = new Dictionary<string, IState<T>>();

        public StateMachine(T refObj)
        {
            obj = refObj;
        }

        public virtual void Initailized()
        {
            if (!statecache.TryGetValue("Idle", out var initState))
            {
                Debug.Log("Don't Exist StateName");
                return;
            }
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

            if (curState == null || !Manager.PlayManager.Instance.IsGamePlay) if (statecache.TryGetValue("Idle", out _state)) return _state;

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

    public class MonsterStateMachine : StateMachine<CommonMonster>
    {
        public MonsterStateMachine(CommonMonster refObj) : base(refObj)
        {
            statecache.Add("Idle", new Enemy.Common.IdleState());
            statecache.Add("Move", new Enemy.Common.MoveState());
            statecache.Add("Attack", new Enemy.Common.AttackState());
            statecache.Add("Hit", new Enemy.Common.HitState());
            statecache.Add("Dead", new Enemy.Common.DeadState());
            Initailized();
        }

        public override void Initailized()
        {
            base.Initailized();
        }
    }

    public class BossStateMachine : StateMachine<BossMonster>
    {
        public BossStateMachine(BossMonster refObj) : base(refObj)
        {
            statecache.Add("Idle", new Enemy.Boss.IdleState());
            statecache.Add("Move", new Enemy.Boss.MoveState());
            statecache.Add("Attack", new Enemy.Boss.AttackState());
            statecache.Add("Dead", new Enemy.Boss.DeadState());
            //Initailized();
        }

        public override void Initailized()
        {
            base.Initailized();
        }
    }
}