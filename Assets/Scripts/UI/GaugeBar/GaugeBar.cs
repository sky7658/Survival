using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public abstract class GaugeBar : MonoBehaviour
    {
        private Image frontBar;
        protected Image GetfrontBar { get { return frontBar; } }
        private Image backBar;
        protected Image GetbackBar { get { return backBar; } }

        [SerializeField] protected float maxGaugeValue;
        [SerializeField] protected float gaugeValue;

        protected virtual float DeltaTime => Time.deltaTime;

        private Utility.CoroutineController cc;

        private void Awake()
        {
            backBar = transform.GetChild(0).GetComponent<Image>();
            frontBar = transform.GetChild(1).GetComponent<Image>();
        }
        public virtual void Initialized(float maxValue)
        {
            if (cc == null)
            {
                cc = new Utility.CoroutineController();
                cc.AddCoroutine("UpdateGaugeBar");
            }
            maxGaugeValue = maxValue;
        }
        public void SetMaxGaugeValue(float value)
        {
            maxGaugeValue = value;
        }
        public abstract void UpdateGaugeBar(float value);
        protected void UpdateGaugeBar(Image first, Image second)
        {
            first.fillAmount = gaugeValue / maxGaugeValue;
            cc.ExecuteCoroutine(GaugeUpdate(first, second), "UpdateGaugeBar");
        }
        private IEnumerator GaugeUpdate(Image first, Image second)
        {
            float _orginValue = second.fillAmount;
            float _elapsed = 0f;
            float _fillTime = 0.4f;

            while (_elapsed < _fillTime)
            {
                second.fillAmount = Mathf.Lerp(_orginValue, first.fillAmount, (_elapsed += DeltaTime) / _fillTime);
                yield return null;
            }
            second.fillAmount = first.fillAmount;
            yield break;
        }
    }
}