using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class GaugeBar : MonoBehaviour
    {
        private Image frontBar;
        private Image backBar;

        [SerializeField] private float maxGaugeValue;
        [SerializeField] private float gaugeValue;
        protected float GaugeValue
        {
            get { return gaugeValue; }
            set
            {
                float _value = value;
                if (set)
                {
                    if (value >= maxGaugeValue)
                    {
                        frontBar.fillAmount = 0f;
                        backBar.fillAmount = 0f;
                        _value = value - maxGaugeValue;
                    }
                }
                gaugeValue = Mathf.Clamp(_value, 0, maxGaugeValue);
            }
        }
        private bool set;
        private Utility.CoroutineController cc;

        private void Awake()
        {
            backBar = transform.GetChild(0).GetComponent<Image>();
            frontBar = transform.GetChild(1).GetComponent<Image>();
        }
        public void Initialized(float maxValue, bool set = false)
        {
            cc = new Utility.CoroutineController();

            maxGaugeValue = maxValue;
            if (set) GaugeValue = 0f;
            else GaugeValue = maxValue;
            this.set = set;

            cc.AddCoroutine("UpdateGaugeBar");
        }
        public void SetMaxGaugeValue(float value)
        {
            maxGaugeValue = value;
        }
        public virtual void UpdateGaugeBar(float value)
        {
            GaugeValue += value;

            Image _first;
            Image _second;

            if (value > 0)
            {
                _first = backBar;
                _second = frontBar;
            }
            else
            {
                _first = frontBar;
                _second = backBar;
            }

            _first.fillAmount = GaugeValue / maxGaugeValue;
            cc.ExecuteCoroutine(GaugeUpdate(_first, _second), "UpdateGaugeBar");
        }

        private IEnumerator GaugeUpdate(Image first, Image second)
        {
            float _orginValue = second.fillAmount;
            float _elapsed = 0f;
            float _fillTime = 0.4f;

            while (_elapsed < _fillTime)
            {
                second.fillAmount = Mathf.Lerp(_orginValue, first.fillAmount, (_elapsed += Time.deltaTime) / _fillTime);
                yield return null;
            }
            second.fillAmount = first.fillAmount;
            yield break;
        }
    }
}