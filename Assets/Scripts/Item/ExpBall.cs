using LMS.Manager;
using UnityEngine;

namespace LMS.Item
{
    public class ExpBall : MonoBehaviour
    {
        private float amount = 30f;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(User.PlayerInfo.playerTag))
            {
                PlayManager.Instance.UpdateExp(amount);
            }
        }
    }

}