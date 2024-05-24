using UnityEngine.UI;

namespace LMS.UI
{
    public class HpBar : GaugeBar
    {
        public override void Initialized(float maxValue)
        {
            base.Initialized(maxValue);
            gaugeValue = maxValue;
            GetfrontBar.fillAmount = 1f;
            GetbackBar.fillAmount = 1f;
        }
        public override void UpdateGaugeBar(float value)
        {
            var _value = value - gaugeValue;

            Image _first;
            Image _second;

            if (_value > 0)
            {
                _first = GetbackBar;
                _second = GetfrontBar;
            }
            else
            {
                _first = GetfrontBar;
                _second = GetbackBar;
            }
            gaugeValue = value;
            UpdateGaugeBar(_first, _second);
        }
    }
}
