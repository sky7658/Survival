using System.Collections;
using UnityEngine;
using LMS.State;
using LMS.Utility;

namespace LMS.Enemy
{
    public abstract class CommonMonster : Monster
    {
        private MonsterStateMachine stateM;

        public override bool IsAttackAble { get { return base.IsAttackAble && isAtkCool; } } // ������ ��������
        public override void EndAtk()
        {
            base.EndAtk();
            isAtkCool = false;
        }

        private float atkCoolTime;
        private bool isAtkCool; // ��Ÿ���� á�°�
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
        protected override void Attack(Vector2 targetPos) => cc.ExecuteCoroutine(AttackMotion(targetPos), "Attack");
        protected abstract IEnumerator AttackMotion(Vector2 targetPos);

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
        protected override void OnEnable()
        {
            stateM.Initailized();
            isAtkCool = true;
            hit = false;
            base.OnEnable();
        }

        public override void Initialized()
        {
            base.Initialized();

            stateM = new MonsterStateMachine(this);

            if (!MonsterInfo.monsterAtkRanges.TryGetValue(ObjectName, out var atkRange))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkRange = 0f;
            }
            else AtkRange = atkRange;
            if (!MonsterInfo.mCoolTimes.TryGetValue(ObjectName, out atkCoolTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                atkCoolTime = 0f;
            }
            if (!MonsterInfo.mAtkTimes.TryGetValue(ObjectName, out var atkTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                AtkTime = 0f;
            }
            else AtkTime = atkTime;

            cc.ExecuteCoroutine(AttackCoolTime(), "AttackCoolTime");
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
            cc.AddCoroutine("Hit");
            cc.AddCoroutine("AttackCoolTime");
        }

        void Update()
        {
            stateM.UpdateState();
        }
    }

}