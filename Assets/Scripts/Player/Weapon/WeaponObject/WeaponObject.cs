using UnityEngine;
using LMS.General;

namespace LMS.User
{
    public abstract class WeaponObject : MonoBehaviour
    {
        protected WeaponInfo wInfo;
        protected float keepTime;
        private string woName;
        protected string GetWoName => woName;
        public virtual void Initialized(WeaponInfo wInfo)
        {
            this.wInfo = wInfo;
            woName = Base.WeaponInfo.wonames[wInfo.wName];
            keepTime = Base.WeaponInfo.woKeepTimes[woName];
        }
        public virtual void Initialized(WeaponInfo wInfo, Vector2 pos)
        {
            this.wInfo = wInfo;
            woName = Base.WeaponInfo.wonames[wInfo.wName];
            keepTime = Base.WeaponInfo.woKeepTimes[woName];
        }
        public virtual void Initialized(WeaponInfo wInfo, Transform target)
        {
            this.wInfo = wInfo;
            woName = Base.WeaponInfo.wonames[wInfo.wName];
            keepTime = Base.WeaponInfo.woKeepTimes[woName];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Enemy.MonsterInfo.monsterTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var enemy))
                {
                    var _knockBackV = collision.transform.position - transform.position;
                    enemy.TakeDamage(wInfo.atk, _knockBackV.normalized);
                }

                if (wInfo.penetrationCnt == -1) return;
                if (--wInfo.penetrationCnt == 0)
                {
                    ReturnObject();
                }
            }
            //if (collision.CompareTag("Out")) 
            //{
            //    ReturnObject();
            //}
        }

        protected abstract void ReturnObject();
    }

}
