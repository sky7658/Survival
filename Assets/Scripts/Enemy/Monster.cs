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

        protected float atk;
        public virtual bool IsAttackAble { get { return !IsChaseAble; } } // 공격이 가능한지
        private bool isAtk; // 공격중인지
        public bool IsAtk { get { return isAtk; } }
        public virtual void EndAtk() => isAtk = false;

        protected virtual float AtkTime { get; set; } // 공격하는 시간
        protected virtual float AtkRange { get; set; }

        public void Attack()
        {
            isAtk = true;
            Attack(targetTrans.position);
        }
        protected abstract void Attack(Vector2 targetPos);

        public bool IsChaseAble
        {
            get
            {
                if (AtkRange < 0) return false;
                return Vector2.Distance(targetTrans.position, transform.position) > AtkRange;
            }
        }
        public void FlipX() => FlipX(targetTrans.position.x - transform.position.x > 0 ? false : true);
        public void ChaseToTarget()
        {
            FlipX();
            Move((targetTrans.position - transform.position).normalized);
        }

        public abstract void ReturnMonster();
        public override void Dead()
        {
            base.Dead();
        }
        protected virtual void OnEnable()
        {
            InitSO();
            isAtk = false;
            SetCollider(true);
        }

        public override void Initialized()
        {
            base.Initialized();
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
            cc.AddCoroutine("Attack");
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
