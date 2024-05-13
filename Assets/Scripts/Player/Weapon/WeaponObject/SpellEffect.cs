using UnityEngine;
using LMS.Enemy;
using LMS.General;
using static UnityEngine.GraphicsBuffer;

namespace LMS.User
{
    public class SpellEffect : MonoBehaviour
    {
        private static Vector2 range = new Vector2(18, 10);
        public void Initialized(WeaponInfo wInfo, Vector2 pos, int objCnt)
        {
            transform.position = pos;
            SerachTarget(wInfo, objCnt);
        }

        private void SerachTarget(WeaponInfo wInfo, int objCnt)
        {
            var targets = Utility.UtilFunctions.TransArrayToList(Physics2D.OverlapBoxAll(
                transform.position, range, 0f, LayerMask.GetMask(MonsterInfo.monsterLayer)));
            for(int i = 0; i < objCnt; i++) 
            {
                if (targets.Count == 0) break;
                int _rand = Random.Range(0, targets.Count);
                if (targets[_rand].TryGetComponent<IDamageable>(out var monster))
                {
                    var _knockBackV = targets[_rand].transform.position - transform.position;

                    monster.TakeDamage(wInfo.atk, _knockBackV.normalized);
                    var _sword = Utility.ObjectPool.Instance.GetObject<Sword>(Base.WeaponInfo.wonames[wInfo.wName]);
                    _sword.Initialized(wInfo, targets[_rand].transform);
                }
                targets.RemoveAt(_rand);
            }
        }

        public void Relase() => Utility.ObjectPool.Instance.ReturnObject(this, Base.WeaponInfo.spellEffectName);
    }
}