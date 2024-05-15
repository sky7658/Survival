using LMS.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public class Slime : CommonMonster
    {
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            float _waitTime = AtkTime / 2f;
            float _atkTIme = AtkTime - _waitTime;
            float _elapsed = 0f;
            bool _flag = false;

            while (_elapsed < _waitTime)
            {
                _elapsed += Time.deltaTime;
                if (IsHit)
                {
                    EndAtk();
                    yield break;
                }
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < _atkTIme)
            {
                if (IsHit)
                {
                    EndAtk();
                    yield break;
                }

                _elapsed += Time.deltaTime;

                var hit = Physics2D.OverlapCircle((Vector2)transform.position + GetColliderCenter(), 1f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null) 
                {
                    if (!_flag && hit.TryGetComponent<IDamageable>(out var obj))
                    {
                        obj.TakeDamage(Atk);
                        _flag = true;
                    }
                }
                yield return null;
            }
            EndAtk();
            yield break;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + GetColliderCenter(), 1f);
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }
}
