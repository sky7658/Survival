using UnityEngine;
using System.Collections.Generic;
using LMS.Enemy;
using LMS.General;

namespace LMS.User
{
    public class SpellEffect : MonoBehaviour
    {
        private static Vector2 range = new Vector2(15, 8);
        public void Initialized(WeaponInfo wInfo, Vector2 pos, int objCnt)
        {
            transform.position = pos;
            SerachTarget(wInfo, objCnt);
        }

        private void SerachTarget(WeaponInfo wInfo, int objCnt)
        {
            var targets = Utility.UtilFunctions.TransArrayToList(Physics2D.OverlapBoxAll(
                transform.position, range, 0f, LayerMask.GetMask(MonsterInfo.monsterLayer)));

            SortedSet<System.Tuple<float, int>> _pq = new SortedSet<System.Tuple<float, int>>();
            for(int i = 0; i < targets.Count; i++) _pq.Add(System.Tuple.Create(Vector2.Distance(targets[i].transform.position, transform.position), i));

            for (int i = 0; i < objCnt; i++)
            {
                if (_pq.Count == 0) break;
                var _index = _pq.Min.Item2;
                if (targets[_index].TryGetComponent<IDamageable>(out var monster))
                {
                    var _knockBackV = targets[_index].transform.position - transform.position;
                    var _range = wInfo.atk * 0.2f;
                    var _atk = Random.Range(wInfo.atk - _range, wInfo.atk + _range + 1);
                    monster.TakeDamage(_atk, _knockBackV.normalized);

                    var _sword = Utility.ObjectPool.Instance.GetObject<Sword>(Base.WeaponInfo.wonames[wInfo.wName]);
                    _sword.Initialized(wInfo, targets[_index].transform);
                }
                _pq.Remove(_pq.Min);
            }
        }

        public void Relase() => Utility.ObjectPool.Instance.ReturnObject(this, Base.WeaponInfo.spellEffectName);
    }
}