using LMS.General;
using System.Collections;
using UnityEngine;

namespace LMS.Enemy
{
    public class Pebble : CommonMonster
    {
        private Transform effect;
        protected override void InitComponent()
        {
            base.InitComponent();
            effect = transform.GetChild(0).GetComponent<Transform>();
        }
        protected override IEnumerator AttackMotion(Vector2 targetPos)
        {
            effect.gameObject.SetActive(true);

            var hit = Physics2D.OverlapCircle(transform.position, 0.8f, LayerMask.GetMask(User.PlayerInfo.playerLayer));
            if (hit != null) 
            { 
                if (hit.TryGetComponent<IDamageable>(out var obj)) obj.TakeDamage(Atk);
            }
            EndAtk();
            Hp = 0;
            yield break;
        }
        public override void ReturnMonster()
        {
            effect.gameObject.SetActive(false);
            Utility.ObjectPool.Instance.ReturnObject(this, ObjectName);
        }
    }

}
