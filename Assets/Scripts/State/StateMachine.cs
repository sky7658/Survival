using UnityEngine;
using LMS.User;
using LMS.General;
using LMS.Enemy;

namespace LMS.State
{
    public abstract class StateMachine<T> where T : Entity
    {
        private T obj;
        private IState<T> curState;
        public StateMachine(T refObj)
        {
            obj = refObj;
            Initialized();
        }
        public void Initialized()
        {
            if (!GetStateCache("Idle", out var initState)) return;
            curState = initState;
            curState.Enter(obj);
        }
        protected abstract bool GetStateCache(string stateName, out IState<T> value);
        public void ChangeState()
        {
            var newState = GetNextState();
            if (newState == null) return;
            if (curState.Equals(newState)) return;

            curState?.Exit(obj);
            curState = newState;
            curState?.Enter(obj);
        }

        private IState<T> GetNextState()
        {
            if (Time.timeScale == 0f) return curState;
            return curState.TransState(obj);
        }

        public void UpdateState()
        {
            curState.Action(obj);
        }
    }

    public class PlayerStateMachine : StateMachine<Player>
    {
        public PlayerStateMachine(Player refObj) : base(refObj) { }
        protected override bool GetStateCache(string stateName, out IState<Player> value)
        {
            value = StateCache.TryGetPlayerStateCache(stateName);
            if (value is null) return false;
            return true;
        }
    }

    public class CommonStateMachine : StateMachine<CommonMonster>
    {
        public CommonStateMachine(CommonMonster refObj) : base(refObj) { }
        protected override bool GetStateCache(string stateName, out IState<CommonMonster> value)
        {
            value = StateCache.TryGetCommomMonsterStateCache(stateName);
            if (value is null) return false;
            return true;
        }
    }

    public class BossStateMachine : StateMachine<BossMonster>
    {
        public BossStateMachine(BossMonster refObj) : base(refObj) { }
        protected override bool GetStateCache(string stateName, out IState<BossMonster> value)
        {
            value = StateCache.TryGetBossMonsterStateCache(stateName);
            if (value is null) return false;
            return true;
        }
    }
}
