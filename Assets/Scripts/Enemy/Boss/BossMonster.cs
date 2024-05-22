using UnityEngine;
using LMS.State;

namespace LMS.Enemy
{

    public abstract class BossMonster : Monster
    {
        public override float Speed
        {
            get
            {
                if (isAtkCool)
                {
                    base.Speed = 10f;
                    return base.Speed;
                }
                else
                {
                    base.Speed = 0f;
                    return base.Speed;
                }
            }
        }

        private BossStateMachine stateM;

        protected bool transformMode;
        public bool TransformMode { get { return transformMode; } }
        
        protected bool specialAtkMode;
        public bool SpecialAttackMode
        {
            get
            {
                return specialAtkMode;
            }
        }
        protected bool transforming;
        public bool IsTrans { get { return transforming; } }
        public bool StartTrans() => transforming = true;
        public bool CompleteTrans() => transforming = false;

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
        protected virtual AttackTypeDelegate<BossMonster> GetAtkType() => atkTypes.GetAttackType();
        protected override void Attack(Vector2 targetPos)
        {
            var nextAtk = GetAtkType();

            if (atkDelegate != null) cc.ExecuteCoroutine(atkDelegate(this, targetPos, AtkTime), "Attack");
            else Debug.Log($"{ObjectName}의 atkDelegate가 Null 입니다.");

            if (nextAtk != null) atkDelegate = nextAtk;
            else Debug.Log($"{ObjectName}의 nextAtk가 Null 입니다.");
        }

        public override bool IsChaseAble
        {
            get
            {
                if (!isAtkCool) return Vector2.Distance(TargetPos, transform.position) > 1f;
                return base.IsChaseAble;
            }
        }

        public override void Initialized()
        {
            base.Initialized();
            stateM = new BossStateMachine(this);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            atkDelegate = GetAtkType();
            stateM.Initailized();
        }
        public void TransformInit()
        {
            StartTrans();
            transformMode = true;
            atkDelegate = GetAtkType();
            Hp = MaxHp;
            Atk *= 1.5f;
        }
        void Update()
        {
            stateM.ChangeState();
        }
        private void FixedUpdate()
        {
            stateM.UpdateState();
        }
    }
}
