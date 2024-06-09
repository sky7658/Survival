using LMS.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class OptionUI : MonoBehaviour
    {
        [Header("# Slider")]
        [SerializeField] private List<Slider> volumeSliders = new List<Slider>();

        [Header("# Toggle")]
        [SerializeField] private List<Toggle> muteToggles = new List<Toggle>();

        [Header("# Button UI")]
        [SerializeField] protected Button exitBtn;

        protected virtual void Awake()
        {
            for (int i = 0; i < 2; i++)
            {
                int _index = i;
                volumeSliders.Add(transform.GetChild(i).GetComponent<Slider>());
                muteToggles.Add(volumeSliders[i].transform.GetChild(4).GetComponent<Toggle>());

                volumeSliders[i].onValueChanged.AddListener((value) => UpdateVolumeAmount(value, _index));
                muteToggles[i].onValueChanged.AddListener((value) => ToggleEvent(value, _index));
            }

            exitBtn.onClick.AddListener(() => ButtonEvent.UIExitEvent(gameObject));
        }

        private void OnEnable()
        {
            if (muteToggles[0].isOn.Equals(!SoundManager.Instance.IsBgmMute)) ToggleEvent(muteToggles[0].isOn, 0);
            else muteToggles[0].isOn = !SoundManager.Instance.IsBgmMute;

            if (muteToggles[1].isOn.Equals(!SoundManager.Instance.IsSfxMute)) ToggleEvent(muteToggles[1].isOn, 1);
            else muteToggles[1].isOn = !SoundManager.Instance.IsSfxMute;
        }

        private void UpdateVolumeAmount(float value, int index)
        {
            if (index == 0) SoundManager.Instance.BgmVolume = value;
            else SoundManager.Instance.SfxVolume = value;

            if (value > 0f) muteToggles[index].isOn = true;
            else muteToggles[index].isOn = false;
        }

        private void ToggleEvent(bool value, int index)
        {
            if (value)
            {
                if (index == 0)
                {
                    SoundManager.Instance.IsBgmMute = false;
                    volumeSliders[index].value = SoundManager.Instance.saveBgmVolume;
                }
                else
                {
                    SoundManager.Instance.IsSfxMute = false;
                    volumeSliders[index].value = SoundManager.Instance.saveSfxVolume;
                }
            }
            else
            {
                if (index == 0) SoundManager.Instance.IsBgmMute = true;
                else SoundManager.Instance.IsSfxMute = true;
                volumeSliders[index].value = 0f;
            }
        }
    }
}
