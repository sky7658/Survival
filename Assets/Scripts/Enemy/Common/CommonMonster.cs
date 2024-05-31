using LMS.State;
using LMS.Utility;
using System.Collections;
using UnityEngine;

namespace LMS.Enemy
{
    public abstract class CommonMonster : Monster
    {
        public static int aliveMonsterCount = 0;
        private MonsterStateMachine stateM;
        protected override void Attack(Vector2 targetPos) => cc.ExecuteCoroutine(AttackMotion(targetPos), "Attack");
        protected abstract IEnumerator AttackMotion(Vector2 targetPos);
        public override bool AttackOut()
        {
            if (IsHit || base.AttackOut()) return true;
            return base.AttackOut();
        }
        public override bool IsChaseAble
        {
            get
            {
                var _dis = Vector2.Distance(TargetPos, transform.position);
                if (_dis > MonsterSpawner._radius + 2f) // Monster�� player�� �Ÿ��� ���� ���� �̻��� �� ���
                {                                       // player�� �̵��ϴ� �������� position �� ����
                    float _newX = transform.position.x;
                    float _newY = transform.position.y;
                    var _moveV = Controller.InputManager.GetMoveVector();
                    if (_moveV.x > 0f) // x�� ��Ī�̵�
                    {
                        if (TargetPos.x > transform.position.x)
                            _newX = TargetPos.x * 1.5f - transform.position.x;
                    }
                    else
                    {
                        if (TargetPos.x < transform.position.x)
                            _newX = TargetPos.x * 1.5f - transform.position.x;
                    }

                    if (_moveV.y > 0f) // y�� ��Ī�̵�
                    {
                        if (TargetPos.y > transform.position.y)
                            _newY = TargetPos.y * 1.5f - transform.position.y;
                    }
                    else
                    {
                        if (TargetPos.y < transform.position.y)
                            _newY = TargetPos.y * 1.5f - transform.position.y;
                    }
                    transform.position = new Vector2(_newX, _newY);
                }
                return _dis > AtkRange;
            }
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
        public override void Dead()
        {
            // ������ ���ܼ� ���� ���͵��� �������� ������ �ʵ���
            var _item = ObjectPool.Instance.GetObject<ItemObject.ExpBall>(ItemObject.ItemInfo.expBallName);
            _item.transform.position = transform.position;

            ObjectPool.Instance.GetObject<UI.Coin>(UI.Coin.coinName).Initialized(transform.position);

            base.Dead();
        }
        protected override void OnEnable()
        {
            aliveMonsterCount++;
            stateM.Initailized();
            hit = false;
            base.OnEnable();
        }
        private void OnDisable()
        {
            aliveMonsterCount--;
        }
        public override void Initialized()
        {
            base.Initialized();

            stateM = new MonsterStateMachine(this);

            if (!MonsterInfo.monsterAtkRanges.TryGetValue(ObjectName, out var atkRange))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                AtkRange = 0f;
            }
            else AtkRange = atkRange;
            if (!MonsterInfo.mAtkTimes.TryGetValue(ObjectName, out var atkTime))
            {
                Debug.Log($"{ObjectName} is not exist in MonsterName");
                AtkTime = 0f;
            }
            else AtkTime = atkTime;
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
            cc.AddCoroutine("Hit");
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