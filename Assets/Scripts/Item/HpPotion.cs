using UnityEngine;
using LMS.General;
using LMS.Utility;

namespace LMS.ItemObject
{
    public class HpPotion : MonoBehaviour
    {
        private float amount = 30f;
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                if (collision.TryGetComponent<IDamageable>(out var _player))
                {
                    _player.Recovery(amount);
                    ObjectPool.Instance.ReturnObject(this, ItemInfo.hpPotionName);
                }
            }
        }
    }

}
