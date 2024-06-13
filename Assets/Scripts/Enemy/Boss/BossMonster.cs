using UnityEngine;
using LMS.State;
using LMS.UI;
using LMS.Manager;

namespace LMS.Enemy
{

    public abstract class BossMonster : Monster
    {
        private GaugeBar hpBar;

        public override float Speed
        {
            get
            {
                if (isAtkCool)
                {
                    base.Speed = MaxSpeed;
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
        public override void TakeDamage(float value, Vector2 vec = default)
        {
            if (transformMode && transforming) value = 0f;
            base.TakeDamage(value, vec);
            hpBar.UpdateGaugeBar(Hp);
        }
        public override void Recovery(float value)
        {
            base.Recovery(value);
            hpBar.UpdateGaugeBar(Hp);
        }

        public override void Initialized()
        {
            hpBar = GameObject.Find("BossHpBar").GetComponent<HpBar>();

            base.Initialized();
            stateM = new BossStateMachine(this);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            if (atkDelegate == null) atkDelegate = GetAtkType();
            stateM.Initailized();

            hpBar.gameObject.SetActive(true);
        }
        private void Start()
        {
            hpBar.Initialized(MaxHp);
        }
        public void TransformInit()
        {
            StartTrans();
            hpBar.Initialized(MaxHp);
            transformMode = true;
            atkDelegate = GetAtkType();
            Hp = MaxHp;
            Atk *= 1.5f;
            CutSceneManager.Instance.StartBossModeCutScene((Vector2)transform.position - PlayManager.Instance.GetPlayerPos);
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
