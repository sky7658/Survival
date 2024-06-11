using System.Collections.Generic;
using UnityEngine;
using LMS.User;
using LMS.Utility;
using LMS.Data;

namespace LMS.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Ŭ���� ��� ���� �� ������Ƽ
        // PlayerData
        public int PlayerMoney 
        {  
            get { return playerData.money; } 
            set { playerData.money = value; }
        }
        public int ClearCount { get { return playerData.count; } }
        #endregion

        #region GameData
        private PlayerData playerData;

        // Ability
        private List<Ability> abilities = new List<Ability>();
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

        #region �ܺ� ��ȣ�ۿ� �޼ҵ�
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
        public void InitPlayerData()
        {
            if (playerData == null) DataManager.LoadData();
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
            playerData = DataManager.LoadData();
            InitPlayerData();
        }
        #endregion
        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}