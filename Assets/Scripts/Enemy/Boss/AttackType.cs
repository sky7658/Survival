using LMS.Enemy.Boss;
using LMS.General;
using System.Collections;
using System.Collections.Generic;
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
            delegateList.Add(Punch);
            delegateList.Add(MagicAttack);
            delegateList.Add(Laser);
            delegateList.Add(Defense);
        }
        private Vector2[] hitBoxPos = new Vector2[2] { new Vector2(-0.9f, -0.4f), new Vector2(0.9f, -0.4f) };
        private IEnumerator Rush(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            Vector2 _hitBoxPos = obj.GetFlipX ? hitBoxPos[0] : hitBoxPos[1];
            Vector2 _hitSize = new Vector2(4f, 2.7f);
            Vector2 _rushArrow = new Vector2(targetPos.x - obj.transform.position.x, 0f).normalized * 5f;
            float _elapsed = 0f;
            float _atkTime = 0.2f;
            float _waitTIme = 0.6f;
            bool _flag = false;

            while (_elapsed < _waitTIme)
            {
                if (obj.AttackOut())
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            obj.Move(_rushArrow);
            while (_elapsed < _atkTime)
            {
                if (obj.AttackOut())
                {
                    obj.EndAtk();
                    obj.Move(Vector2.zero);
                    yield break;
                }
                var hit = Physics2D.OverlapBox((Vector2)obj.transform.position + _hitBoxPos, _hitSize, 0f, 
                    LayerMask.GetMask(User.PlayerInfo.playerLayer));
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
            obj.Move(Vector2.zero);
            _elapsed = 0f;
            while (_elapsed < atkTime - _waitTIme - _atkTime)
            {
                if (obj.AttackOut())
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
                if (obj.AttackOut())
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
                if (obj.AttackOut())
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
                if (obj.AttackOut())
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
                if (obj.AttackOut())
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            Manager.PlayManager.Instance.ShakeCamera(_atkTime);
            while (_elapsed < _atkTime)
            {
                if (obj.AttackOut())
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
                if (obj.AttackOut())
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
            Vector2 _arrow = new Vector2(targetPos.x - obj.transform.position.x, 0f);
            float _elapsed = 0f;
            float _waitTime = 0.7f;

            while (_elapsed < _waitTime)
            {
                if (obj.AttackOut())
                {
                    obj.EndAtk();
                    yield break;
                }
                _elapsed += Time.deltaTime;
                yield return null;
            }
            _elapsed = 0f;
            var _laser = Utility.ObjectPool.Instance.GetObject<Laser>(MonsterInfo.laserName);
            _laser.Initialized(obj.transform.position, _arrow.normalized, obj.Atk);
            
            while (_elapsed < atkTime - _waitTime)
            {
                if (obj.AttackOut())
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

        private IEnumerator Defense(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            float _elapsed = 0f;
            while (_elapsed < atkTime)
            {
                if (obj.AttackOut())
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


