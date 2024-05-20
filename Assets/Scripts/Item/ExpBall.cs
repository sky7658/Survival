using LMS.Manager;
using LMS.User;
using LMS.Utility;
using UnityEngine;

namespace LMS.ItemObject
{
    public class ExpBall : MonoBehaviour
    {
        private float amount = 30f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(PlayerInfo.playerTag))
            {
                PlayManager.Instance.UpdateExp(amount);
                ObjectPool.Instance.ReturnObject(this, ItemInfo.expBallName);
            }
        }
    }

}