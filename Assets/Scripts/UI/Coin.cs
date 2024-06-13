using LMS.Manager;
using LMS.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class Coin : MonoBehaviour
    {
        public static readonly string coinName = "Coin";
        [SerializeField] private float speed;
        private Image img;
        private Vector2 velocity = Vector2.zero;
        private Coroutine coroutine;
        private void Awake()
        {
            img = GetComponent<Image>();
        }
        public void Initialized(Vector2 pos)
        {
            int _amount = GetMoneyAmount();
            transform.position = pos;

            img.sprite = GetMoneySprite(_amount);
            coroutine = UtilCoroutine.ExecuteCoroutine(ChaseTarget(_amount), coroutine);
        }

        private IEnumerator ChaseTarget(int amount)
        {
            float _elapsed = 0f;
            while (_elapsed < 1f)
            {
                _elapsed += Time.unscaledDeltaTime;
                transform.localPosition = Vector2.SmoothDamp(transform.localPosition, new Vector2(610f, 248f), ref velocity, speed, Mathf.Infinity, Time.unscaledDeltaTime);
                yield return null;
            }
            SoundManager.Instance.PlaySFX("GetCoin");
            PlayManager.Instance.UpdateMoney(amount);
            ObjectPool.Instance.ReturnObject(this, coinName);
            yield return null;
        }

        public static Sprite GetMoneySprite(int amount)
        {
            if (amount > 10000) return ResourceManager.GetSprite("Money5");
            if (amount > 1000) return ResourceManager.GetSprite("Money4");
            if (amount > 100) return ResourceManager.GetSprite("Money3");
            if (amount > 10) return ResourceManager.GetSprite("Money2");
            return ResourceManager.GetSprite("Money1");
        }

        private int GetMoneyAmount()
        {
            int _range = 10;
            int _amount = 0;

            for (int i = 0; i < 2; i++)
            {
                _amount = Random.Range(_range / 10, _range + 1);
                if (_amount != _range) return _amount;
                _range *= 10;
            }

            return _amount;
        }
    }
}