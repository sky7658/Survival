using System.Collections;
using UnityEngine;
using LMS.General;
using LMS.State;
using LMS.Utility;
using Unity.Burst.CompilerServices;

namespace LMS.Enemy
{
    public abstract class Monster : Entity
    {
        public IDamageable target;
        [SerializeField] private Transform targetTrans;
        private MonsterStateMachine stateM;

        protected float atk;
        public bool IsAttackAble // 공격이 가능한지
        {
            get
            {
                bool set = !IsChaseAble && isAtkCool;
                return set;
            }
        }
        private bool isAtk; // 공격중인지
        public bool IsAtk { get { return isAtk; } }
        protected void EndAtk()
        {
            isAtkCool = false;
            isAtk = false;
        }
        private float atkCoolTime;
        private float atkTime; // 공격하는 시간
        protected float AtkTime { get { return atkTime; } }
        private bool isAtkCool; // 쿨타임이 찼는가
        private IEnumerator AttackCoolTime()
        {
            while (true)
            {
                if (!isAtkCool)
                {
                    yield return UtilFunctions.WaitForSeconds(atkCoolTime);
                    isAtkCool = true;
                }
                yield return null;
            }
        }
        public void Attack()
        {
            isAtk = true;
            cc.ExecuteCoroutine(AttackMotion(targetTrans.position), "Attack");
        }
        protected abstract IEnumerator AttackMotion(Vector2 targetPos);

        public bool IsChaseAble => Vector2.Distance(targetTrans.position, transform.position) > MonsterInfo.monsterAtkRange;
        public void FlipX() => FlipX(targetTrans.position.x - transform.position.x > 0 ? false : true);
        public void ChaseToTarget()
        {
            FlipX();
            Move((targetTrans.position - transform.position).normalized);
        }

        private bool hit;
        public bool IsHit { get { return hit; } }
        public void EndHit() => hit = false;

        private Vector2 knockBackV = Vector2.zero;
        public void KnockBack()
        {
            cc.ExecuteCoroutine(KnockBack(knockBackV), "Hit");
        }
        private IEnumerator KnockBack(Vector2 vec)
        {
            if (!MonsterInfo.knockBackTime.TryGetValue(ObjectName, out var _knockBackTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterNames");
                yield break;
            }

            Move(vec);
            yield return UtilFunctions.WaitForSeconds(_knockBackTime);
            Move(Vector2.zero);
            hit = false;
            yield break;
        }
        public override void TakeDamage(float value, Vector2 vec)
        {
            hit = true;
            knockBackV = vec;
            base.TakeDamage(value, vec);
        }

        public abstract void ReturnMonster();
        public override void Dead()
        {
            base.Dead();
        }
        private void OnEnable()
        {
            InitSO();
            stateM.Initailized();

            isAtk = false;
            isAtkCool = true;
            hit = false;

            SetCollider(true);
        }

        public override void Initialized()
        {
            base.Initialized();

            stateM = new MonsterStateMachine(this); // StartInit

            if (!MonsterInfo.mCoolTimes.TryGetValue(ObjectName, out atkCoolTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkCoolTime = 0f;
            }
            if (!MonsterInfo.mAtkTimes.TryGetValue(ObjectName, out atkTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkTime = 0f;
            }

            cc.ExecuteCoroutine(AttackCoolTime(), "AttackCoolTime");
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
            cc.AddCoroutine("Hit");
            cc.AddCoroutine("Attack");
            cc.AddCoroutine("AttackCoolTime");
        }
        private void Awake()
        {
            InitComponent();
            targetTrans = GameObject.Find("Player").GetComponent<Transform>(); // 이거 수정해야함
            target = targetTrans.GetComponent<IDamageable>();

            Initialized();
        }
        void Update()
        {
            stateM.UpdateState();
        }
    }
}
