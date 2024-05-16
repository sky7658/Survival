using LMS.General;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LMS.Enemy
{
    public class Bat : CommonMonster
    {
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            Vector2 _originPos = transform.position;
            float _elapsed = 0f;
            float _waitTime = AtkTime / 5f;
            float _rushTime = AtkTime / 5f;
            bool _flag = false;

            yield return Utility.UtilFunctions.WaitForSeconds(_waitTime);

            while (_elapsed < _rushTime)
            {
                if (IsHit)
                {
                    EndAtk();
                    yield break;
                }

                _elapsed += Time.deltaTime;
                transform.position = Vector2.Lerp(_originPos, targetPos, _elapsed / _rushTime);
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < _rushTime)
            {
                if (IsHit)
                {
                    EndAtk();
                    yield break;
                }
                var hit = Physics2D.OverlapCircle(transform.position, 0.4f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null)
                {
                    if (!_flag && hit.TryGetComponent<IDamageable>(out var _obj))
                    {
                        _obj.TakeDamage(Atk);
                        _flag = true;
                    }
                }
                transform.position = Vector2.Lerp(targetPos, _originPos, (_elapsed += Time.deltaTime) / _rushTime);
                yield return null;
            }
            transform.position = _originPos;
            EndAtk();
            yield break;
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }

}
