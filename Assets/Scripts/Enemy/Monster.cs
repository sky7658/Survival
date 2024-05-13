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

        private float atk;
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
        public void StartAtk() => isAtk = true;
        public void EndAtk()
        {
            isAtkCool = false;
            isAtk = false;
        }
        private float atkCoolTime;
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
        public bool IsChaseAble => Vector2.Distance(targetTrans.position, transform.position) > MonsterInfo.monsterAtkRange;
        public void ChaseToTarget()
        {
            FlipX(targetTrans.position.x - transform.position.x > 0 ? false : true);
            Move((targetTrans.position - transform.position).normalized);
        }

        private bool hit;
        public bool IsHit { get { return hit; } }
        public void EndHit() => hit = false;
        public override void TakeDamage(float value, Vector2 vec)
        {
            Debug.Log(vec);
            cc.ExecuteCoroutine(KnockBack(vec), "Hit");
            base.TakeDamage(value, vec);
        }
        private IEnumerator KnockBack(Vector2 vec)
        {
            if (!MonsterInfo.knockBackTime.TryGetValue(ObjectName, out var _knockBackTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterNames");
                yield break;
            }

            Move(vec);
            hit = true;
            yield return UtilFunctions.WaitForSeconds(_knockBackTime);
            Move(Vector2.zero);
            hit = false;
            yield break;
        }

        public abstract void ReturnMonster();
        public override void Dead()
        {
            base.Dead();
            ReturnMonster();
        }

        public override void Initialized()
        {
            base.Initialized();

            stateM = new MonsterStateMachine(this); // StartInit

            isAtk = false;
            isAtkCool = true;
            hit = false;
            if (!MonsterInfo.mCoolTimes.TryGetValue(ObjectName, out atkCoolTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkCoolTime = 0f;
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
            target = targetTrans.GetComponent<IDamageable>();
        }
        private void Start()
        {
            Initialized();
        }

        void Update()
        {
            stateM.UpdateState();
        }
    }
}
