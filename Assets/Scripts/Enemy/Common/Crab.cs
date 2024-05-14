using LMS.General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public class Crab : CommonMonster
    {
        Vector2[] tongsPos = new Vector2[2] { new Vector2(-0.5f, -0.35f), new Vector2(0.5f, -0.35f)};
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            Vector2 _tongPos = GetFlipX ? tongsPos[0] : tongsPos[1];
            float _elapsed = 0f;
            float _waitTime = AtkTime / 5f * 2f;
            float _atkTime = AtkTime - _waitTime;
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
            while (_elapsed < _atkTime)
            {
                if (IsHit)
                {
                    EndAtk();
                    yield break;
                }

                _elapsed += Time.deltaTime;

                var hit = Physics2D.OverlapBox((Vector2)transform.position + _tongPos, Vector2.one, 0f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null) 
                { 
                    if (hit.TryGetComponent<IDamageable>(out var obj) && !_flag)
                    {
                        obj.TakeDamage(atk);
                        _flag = true;
                    }
                }
                yield return null;
            }
            EndAtk();
            yield break;
        }
        public override void ReturnMonster() => Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
    }
}