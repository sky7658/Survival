using LMS.General;
using UnityEngine;

namespace LMS.Enemy.Boss
{
    public class Laser : MonoBehaviour
    {
        private float atk;
        private float speed = 10f;
        private Rigidbody2D rig;
        public void Initialized(Vector2 pos, Vector2 arrow, float atk)
        {
            this.atk = atk;
            transform.position = pos;
            rig.velocity = arrow * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var obj))
                {
                    obj.TakeDamage(atk);
                    Utility.ObjectPool.Instance.ReturnObject(this, "Laser");
                }
            }
        }
    }
}
