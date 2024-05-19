using UnityEngine;
using LMS.General;
using Unity.VisualScripting;

namespace LMS.User
{
    public abstract class WeaponObject : MonoBehaviour
    {
        Transform t;
        protected WeaponInfo wInfo;
        protected float keepTime;
        private string woName;
        protected string GetWoName => woName;
        public virtual void Initialized(WeaponInfo wInfo)
        {
            this.wInfo = wInfo;
            if (!Base.WeaponInfo.wonames.TryGetValue(wInfo.wName, out woName))
            {
                Debug.Log($"{wInfo.wName} is not exist in WeaponNames");
                woName = "NULL";
            }
            if (!Base.WeaponInfo.woKeepTimes.TryGetValue(woName, out keepTime))
            {
                Debug.Log($"{woName} is not exist in WeaponObjectNames");
                keepTime = 0f;
            }
            t = GameObject.Find("Player").GetComponent<Transform>(); // 없애주세용x
        }
        public virtual void Initialized(WeaponInfo wInfo, Vector2 pos)
        {
            Initialized(wInfo);
        }
        public virtual void Initialized(WeaponInfo wInfo, Transform target)
        {
            Initialized(wInfo);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Enemy.MonsterInfo.monsterTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var enemy))
                {
                    if (wInfo.penetrationCnt == 0) return;
                    --wInfo.penetrationCnt;
                    var _knockBackV = collision.transform.position - t.position; // 수정해주세용
                    enemy.TakeDamage(wInfo.atk, _knockBackV.normalized);
                }

                if (wInfo.penetrationCnt == -1) return;
                if (wInfo.penetrationCnt == 0)
                {
                    ReturnObject();
                    return;
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