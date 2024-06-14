using System.Collections.Generic;
using UnityEngine;
using LMS.User;
using LMS.Utility;
using LMS.Data;
using UnityEditor;

namespace LMS.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region 클래스 멤버 변수 및 프로퍼티
        // PlayerData
        public int PlayerMoney 
        {  
            get { return playerData.money; } 
            set { playerData.money = value; }
        }
        public int ClearCount { get { return playerData.count; } }
        #endregion

        #region GameData
        private static PlayerData playerData;

        // Ability
        private static List<Ability> abilities = new List<Ability>();
        public float GetAbilityValue(string name) => TryGetAbility(name).AbilityValue;
        public void UpdateGameData(int money, int clearCount)
        {
            PlayerMoney += money;
            playerData.count += clearCount;
        }
        private void SaveGameData()
        {
            //Ability
            for (int i = 0; i < abilities.Count; i++) playerData.level[i] = abilities[i].GetLevel;

            // Sound
            playerData.volumes[0] = SoundManager.Instance.BgmVolume;
            playerData.saveVolumes[0] = SoundManager.Instance.saveBgmVolume;
            playerData.isMute[0] = SoundManager.Instance.IsBgmMute;

            playerData.volumes[1] = SoundManager.Instance.SfxVolume;
            playerData.saveVolumes[1] = SoundManager.Instance.saveSfxVolume;
            playerData.isMute[1] = SoundManager.Instance.IsSfxMute;

            DataManager.SaveData(playerData);
        }
        #endregion

        #region 외부 상호작용 메소드
        public Ability TryGetAbility(string name)
        {
            if (name.Equals(Shoes.abilityName)) return abilities[0];
            if (name.Equals(Hat.abilityName)) return abilities[1];
            if (name.Equals(Candy.abilityName)) return abilities[2];

            Debug.Log($"{name} is not exist in Abilities");
            return null;
        }
        #endregion

        #region Init
        
        [RuntimeInitializeOnLoadMethod]
        private static void InitPlayerData()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 120;
#else 
            Application.targetFrameRate = -1;
#endif
            playerData = DataManager.LoadData();

            // Ability
            abilities.Add(new Shoes(PlayerInfo.playerSO.BasicSpeed, playerData.level[0]));
            abilities.Add(new Hat(PlayerInfo.playerSO.BasicDefense, playerData.level[1]));
            abilities.Add(new Candy(PlayerInfo.playerSO.MaxHp, playerData.level[2]));

            // Sound
            SoundManager.Instance.BgmVolume = playerData.volumes[0];
            SoundManager.Instance.saveBgmVolume = playerData.saveVolumes[0];
            SoundManager.Instance.IsBgmMute = playerData.isMute[0];

            SoundManager.Instance.SfxVolume = playerData.volumes[1];
            SoundManager.Instance.saveSfxVolume = playerData.saveVolumes[1];
            SoundManager.Instance.IsSfxMute = playerData.isMute[1];
        }
        protected override bool UseDontDestroy() => true;
        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Called GameManager Awake");
        }
#endregion
        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}