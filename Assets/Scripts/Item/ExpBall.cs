using LMS.Manager;
using LMS.User;
using LMS.Utility;
using System.Collections;
using UnityEngine;

namespace LMS.ItemObject
{
    public class ExpBall : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float amount = 30f;
        private Coroutine coroutine;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals(PlayerInfo.playerTag))
            {
                SoundManager.Instance.PlaySFX("GetExp");
                PlayManager.Instance.UpdateExp(amount);
                ObjectPool.Instance.ReturnObject(this, ItemInfo.expBallName);
            }
        }

        private void OnEnable()
        {
            PlayManager.Instance.AddExpBall(this);
        }

        private void OnDisable()
        {
            if (coroutine != null) UtilCoroutine.OffCoroutine(ref coroutine);
            PlayManager.Instance.DeleteExpBall(this);
        }

        public void AutoGet() => coroutine = UtilCoroutine.ExecuteCoroutine(ChasePlayer(), coroutine);
        private IEnumerator ChasePlayer()
        {
            while (true)
            {
                var _direction = (PlayManager.Instance.GetPlayerPos - (Vector2)transform.position).normalized;
                transform.Translate(_direction * 7f * Time.deltaTime);
                yield return null;
            }
        }
    }

}