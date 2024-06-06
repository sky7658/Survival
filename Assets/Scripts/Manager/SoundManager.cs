using LMS.Utility;
using UnityEngine;

namespace LMS.Manager
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        protected override bool UseDontDestroy() => true;

        [Header("# Audio Source")]
        [SerializeField] private AudioSource bgmSc;
        [SerializeField] private AudioSource sfxSc;

        [Header("# Volume")]
        [SerializeField] private float bgmVolume = 1f;
        public float BgmVolume
        {
            get { return bgmVolume; }
            set 
            {
                bgmVolume = value;
                SetBGMVolume();
                if (!IsBgmMute && BgmVolume > 0f) saveBgmVolume = BgmVolume;
            }
        }
        [SerializeField] private float sfxVolume = 1f;
        public float SfxVolume
        {
            get { return sfxVolume; }
            set 
            { 
                sfxVolume = value;
                SetSFXVolume();
                if (!isSfxMute && SfxVolume > 0f) saveSfxVolume = SfxVolume;
            }
        }
        public float saveBgmVolume = 1f;
        public float saveSfxVolume = 1f;
        [SerializeField] private bool isBgmMute = false;
        public bool IsBgmMute
        {
            get { return isBgmMute; }
            set { isBgmMute = value; }
        }
        [SerializeField] private bool isSfxMute = false;
        public bool IsSfxMute
        {
            get { return isSfxMute; }
            set { isSfxMute = value; }
        }

        private void SetBGMVolume() => bgmSc.volume = BgmVolume;
        private void SetSFXVolume() => sfxSc.volume = SfxVolume;

        public void PlayBGM()
        {

        }
        public void PlaySFX()
        {

        }
    }
}
