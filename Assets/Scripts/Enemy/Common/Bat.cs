using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LMS.Enemy
{
    public class Bat : Monster
    {
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            Vector2 _originPos = transform.position;
            float _elapsed = 0f;
            float _waitTime = AtkTime / 5f;
            float _rushTime = AtkTime / 5f;

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

                _elapsed += Time.deltaTime;
                transform.position = Vector2.Lerp(targetPos, _originPos, _elapsed / _rushTime);
                yield return null;
            }
            transform.position = _originPos;
            EndAtk();
            yield break;
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }

}
