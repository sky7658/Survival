using UnityEngine;

namespace LMS.UI
{
    public class ExpBar : GaugeBar
    {
        protected override float DeltaTime => Time.unscaledDeltaTime;
        public override void Initialized(float maxValue)
        {
            base.Initialized(maxValue);
            gaugeValue = 0f;
            GetfrontBar.fillAmount = 0f;
            GetbackBar.fillAmount = 0f;
        }
        public override void UpdateGaugeBar(float value)
        {
            var _value = value - gaugeValue;

            if (_value < 0)
            {
                GetfrontBar.fillAmount = 0f;
                GetbackBar.fillAmount = 0f;
            }
            gaugeValue = value;
            UpdateGaugeBar(GetbackBar, GetfrontBar);
        }
    }
}
