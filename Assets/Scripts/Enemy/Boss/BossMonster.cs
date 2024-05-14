using UnityEngine;
using LMS.State;

namespace LMS.Enemy
{

    public abstract class BossMonster : Monster
    {
        private BossStateMachine stateM;

        protected bool transformMode;
        public bool TransformMode { get { return transformMode; } }
        protected bool transition;
        public bool IsTransition { get { return transition; } }

        protected AttackType<BossMonster> atkTypes;
        protected AttackTypeDelegate<BossMonster> atkDelegate;
        protected override float AtkTime 
        { 
            get
            {
                if (atkDelegate == null)
                {
                    Debug.Log("atkDelegate is Null");
                    return 0f;
                }
                if (MonsterInfo.bossAtkTimes.TryGetValue(ObjectName, out var atkTimes))
                {
                    if (!atkTimes.TryGetValue(atkDelegate.Method.Name, out var time))
                    {
                        Debug.Log($"{atkDelegate.Method.Name} is not exist in Delegate Method names of {ObjectName}");
                        return 0f;
                    }
                    return time;
                }
                else
                {
                    Debug.Log($"{ObjectName} is not exist in MonsterNames");
                    return 0f;
                }
            }
        }
        protected override float AtkRange
        {
            get
            {
                if (atkDelegate == null)
                {
                    Debug.Log("atkDelegate is Null");
                    return 0f;
                }
                if (MonsterInfo.bossAtkRanges.TryGetValue(ObjectName, out var atkRanges))
                {
                    if (!atkRanges.TryGetValue(atkDelegate.Method.Name, out var range))
                    {
                        Debug.Log($"{atkDelegate.Method.Name} is not exist in Delegate Method names of {ObjectName}");
                        return 0f;
                    }
                    return range;
                }
                else
                {
                    Debug.Log($"{ObjectName} is not exist in MonsterNames");
                    return 0f;
                }
            }
        }
        protected override void Attack(Vector2 targetPos)
        {
            var nextAtk = atkTypes.GetAttackType();

            if (atkDelegate != null) cc.ExecuteCoroutine(atkDelegate(this, targetPos, AtkTime), "Attack");
            else Debug.Log($"{ObjectName}의 atkDelegate가 Null 입니다.");

            if (nextAtk != null) atkDelegate = nextAtk;
            else Debug.Log($"{ObjectName}의 nextAtk가 Null 입니다.");
        }

        public override void Initialized()
        {
            base.Initialized();
            stateM = new BossStateMachine(this);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            stateM.Initailized();
            atkDelegate = atkTypes.GetAttackType();
        }

        void Update()
        {
            stateM.UpdateState();
        }
    }
}
