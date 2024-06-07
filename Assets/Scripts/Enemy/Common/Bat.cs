using LMS.General;
using System.Collections;
using UnityEngine;

namespace LMS.Enemy
{
    public class Bat : CommonMonster
    {
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            float _elapsed = 0f;
            float _waitTime = AtkTime / 5f;
            float _rushTime = AtkTime / 5f;
            bool _flag = false;

            while (_elapsed < _waitTime)
            {
                if (AttackOut())
                {
                    EndAtk();
                    yield break;
                }

                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            Move((TargetPos - (Vector2)transform.position).normalized * 3f);
            while (_elapsed < _rushTime)
            {
                if (AttackOut())
                {
                    EndAtk();
                    break;
                }

                var hit = Physics2D.OverlapCircle(transform.position, 0.4f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null)
                {
                    if (!_flag && hit.TryGetComponent<IDamageable>(out var obj))
                    {
                        obj.TakeDamage(Atk);
                        _flag = true;
                    }
                }

                _elapsed += Time.deltaTime;
                yield return null;
            }
            Move(Vector2.zero);
            EndAtk();
            yield break;
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }

}
