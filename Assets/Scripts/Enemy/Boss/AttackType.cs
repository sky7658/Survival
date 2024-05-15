using LMS.General;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LMS.Enemy
{
    public delegate IEnumerator AttackTypeDelegate<T>(T obj, Vector2 targetPos, float atkTime) where T : BossMonster;
    public class AttackType<T> where T : BossMonster
    {
        protected List<AttackTypeDelegate<T>> delegateList = new List<AttackTypeDelegate<T>>();

        public AttackTypeDelegate<T> GetAttackType()
        {
            if (delegateList.Count == 0) return null;
            var _rand = Random.Range(0, delegateList.Count);
            return delegateList[_rand];
        }
    }

    public class GolemAttackType : AttackType<BossMonster>
    {
        public GolemAttackType() : base()
        {
            delegateList.Add(Rush);
            //delegateList.Add(Punch);
            delegateList.Add(MagicAttack);
        }

        private IEnumerator Rush(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            Vector2 _originPos = obj.transform.position;
            Vector2 _rushPos = new Vector2(targetPos.x - obj.transform.position.x, 0f).normalized * 5f;
            float _elapsed = 0f;
            float _atkTime = 0.2f;
            float _waitTIme = 0.6f;

            while (_elapsed < _waitTIme)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }

                obj.transform.position = Vector2.Lerp(_originPos, _originPos + _rushPos, (_elapsed += Time.deltaTime) / _atkTime);
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < atkTime - _waitTIme - _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            obj.EndAtk();
            yield break;
        }

        private IEnumerator Punch(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            float _elapsed = 0f;
            float _atkTime = 0.3f;
            float _waitTime = 0.5f;
            bool _flag = false;

            while (_elapsed < _waitTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }

                var hit = Physics2D.OverlapCircle(obj.transform.position, 2f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null)
                {
                    if (!_flag && hit.TryGetComponent<IDamageable>(out var _obj))
                    {
                        _obj.TakeDamage(obj.Atk);
                        _flag = true;
                    }
                }

                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < atkTime - _waitTime - _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            obj.EndAtk();
            yield break;
        }

        private IEnumerator MagicAttack(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            float _elapsed = 0f;
            float _atkTime = 0.5f;
            float _waitTime = 0.8f;
            bool _flag = false;

            while (_elapsed < _waitTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }

                var hit = Physics2D.OverlapCircle(obj.transform.position, 7f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
                if (hit != null)
                {
                    if (!_flag && hit.TryGetComponent<IDamageable>(out var _obj))
                    {
                        _obj.TakeDamage(obj.Atk);
                        _flag = true;
                    }
                }

                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            while (_elapsed < atkTime - _waitTime - _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            obj.EndAtk();
            yield break;
        }

        private IEnumerator Laser(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            float _elapsed = 0f;
            float _waitTime = 0.7f;

            while (_elapsed < _waitTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            
            while (_elapsed < atkTime - _waitTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            obj.EndAtk();
            yield break;
        }
    }
}
