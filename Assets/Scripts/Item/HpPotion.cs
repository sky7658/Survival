using UnityEngine;
using LMS.General;

namespace LMS.Item
{
    public class HpPotion : MonoBehaviour
    {
        private float amount = 30f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var _player))
                    _player.Recovery(amount);
            }
        }
    }

}
