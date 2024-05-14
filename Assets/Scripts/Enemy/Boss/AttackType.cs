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
            //delegateList.Add(Punch);
            //delegateList.Add(MagicAttack);
        }

        private IEnumerator Rush(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            Vector2 _rushVector = new Vector2(targetPos.x - obj.transform.position.x, 0f);
            float _elapsed = 0f;
            float _waitTIme = 0.1f;
            float _atkTime = atkTime - _waitTIme;

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
            obj.Move(_rushVector);
            while (_elapsed < _atkTime)
            {
                if (obj.Hp <= 0f)
                {
                    obj.EndAtk();
                    yield break;
                }
                yield return null;
            }
            obj.Move(Vector2.zero);
            obj.EndAtk();
            yield break;
        }

        private IEnumerator Punch(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            Debug.Log("저는");
            yield break;
        }

        private IEnumerator MagicAttack(BossMonster obj, Vector2 targetPos, float atkTime)
        {
            Debug.Log("이민서입니다.");
            yield break;
        }
    }
}
