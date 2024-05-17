using System.Collections;
using UnityEngine;
using LMS.General;
using LMS.Utility;

namespace LMS.Enemy
{
    public abstract class Monster : Entity
    {
        public IDamageable target;
        [SerializeField] private Transform targetTrans;
        protected Vector2 TargetPos { get { return targetTrans.position; } }

        private float atk;
        public float Atk 
        { 
            get 
            {
                var _range = atk * 0.3f;
                return atk + Random.Range(atk - _range, atk + _range); 
            }
            protected set { atk = value; }
        }
        public virtual bool IsAttackAble { get { return !IsChaseAble && isAtkCool; } } // 공격이 가능한지
        private bool isAtk; // 공격중인지
        public bool IsAtk { get { return isAtk; } }
        public virtual void EndAtk()
        {
            isAtkCool = false;
            isAtk = false;
        }
        private float atkCoolTime;
        protected bool isAtkCool; // 쿨타임이 찼는가
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

        protected virtual float AtkTime { get; set; } // 공격하는 시간
        protected virtual float AtkRange { get; set; }
        public void Attack()
        {
            isAtk = true;
            Attack(targetTrans.position);
        }
        protected abstract void Attack(Vector2 targetPos);
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsAtk) return;
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var obj))
                {
                    obj.TakeDamage(Atk);
                }
            }
        }
        public virtual bool AttackOut()
        {
            if (Hp <= 0) return true;
            return false;
        }

        public virtual bool IsChaseAble
        {
            get
            {
                bool _flag = Vector2.Distance(TargetPos, transform.position) > AtkRange;
                if (AtkRange < 0) return false;
                return Vector2.Distance(TargetPos, transform.position) > AtkRange;
            }
        }
        public void FlipX() => FlipX(TargetPos.x - transform.position.x > 0 ? false : true);
        public void ChaseToTarget()
        {
            FlipX();
            Move((TargetPos - (Vector2)transform.position).normalized);
        }

        public abstract void ReturnMonster();
        public override void Dead()
        {
            base.Dead();
        }
        protected virtual void OnEnable()
        {
            InitSO();
            isAtkCool = false;
            isAtk = false;
            SetCollider(true);
        }

        public override void Initialized()
        {
            base.Initialized();
            if (!MonsterInfo.mCoolTimes.TryGetValue(ObjectName, out atkCoolTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkCoolTime = 0f;
            }
            cc.ExecuteCoroutine(AttackCoolTime(), "AttackCoolTime");

            Atk = 1f; // 수정해주세용
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
            cc.AddCoroutine("Attack");
            cc.AddCoroutine("AttackCoolTime");
        }
        protected override void InitComponent()
        {
            base.InitComponent();
            targetTrans = GameObject.Find("Player").GetComponent<Transform>(); // 이거 수정해야함
            target = targetTrans.GetComponent<IDamageable>();
        }
        private void Awake()
        {
            InitComponent();
            Initialized();
        }
    }
}
