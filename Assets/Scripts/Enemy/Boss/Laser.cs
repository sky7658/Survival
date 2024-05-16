using LMS.General;
using UnityEngine;

namespace LMS.Enemy.Boss
{
    public class Laser : MonoBehaviour
    {
        private float atk;
        private float speed = 12f;
        private Rigidbody2D rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody2D>();
        }

        public void Initialized(Vector2 pos, Vector2 arrow, float atk)
        {
            float axis = Mathf.Atan2(arrow.y, arrow.x) * Mathf.Rad2Deg;

            this.atk = atk;
            transform.position = pos;
            transform.rotation = Quaternion.Euler(0, 0, axis);
            rig.velocity = arrow * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var obj))
                {
                    obj.TakeDamage(atk);
                    Utility.ObjectPool.Instance.ReturnObject(this, MonsterInfo.laserName);
                }
            }
        }
    }
}