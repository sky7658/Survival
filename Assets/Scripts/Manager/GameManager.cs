using System.Collections.Generic;
using UnityEngine;
using LMS.User;
using LMS.Utility;

namespace LMS.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region 클래스 멤버 변수 및 프로퍼티
        // Ability
        private Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();

        // PlayerData
        private int pMoney = 10000; // 임시 데이터
        public int GetPlayerMoney {  get { return pMoney; } }
        #endregion

        #region 외부 상호작용 메소드
        // Ability
        private Ability TryGetAbility(string name)
        {
            if (!abilities.TryGetValue(name, out var _ability))
            {
                Debug.Log($"{name} is not exist in Abilities");
                return null;
            }
            return _ability;
        }
        public float GetAbilityValue(string name) => TryGetAbility(name).AbilityValue;
        public int GetAbilityLevel(string name) => TryGetAbility(name).GetLevel;
        public int GetAbilityNextPrice(string name) => TryGetAbility(name).GetPriceByNextLevel();
        public float GetAbilityNextRatio(string name) => TryGetAbility(name).GetRatioByNextLevel();
        public bool TryUpgradeAbility(string name)
        {
            var _price = GetAbilityNextPrice(name);
            if (GetPlayerMoney < _price)
            {
                Debug.Log("돈 부족");
                return false;
            }
            TryGetAbility(name).UpgradeAbility();
            pMoney -= _price;
            return true;
        }
        #endregion

        #region Init
        public void InitPlayerAbility()
        {
            // PlayerData를 불러와서 인스턴스 생성할 예정
            abilities.Add(Shoes.abilityName, new Shoes(PlayerInfo.playerSO.BasicSpeed, 0));
            abilities.Add(Hat.abilityName, new Hat(PlayerInfo.playerSO.BasicDefense));
            abilities.Add(Candy.abilityName, new Candy(PlayerInfo.playerSO.MaxHp));
        }
        protected override bool UseDontDestroy() => true;
        protected override void Awake()
        {
            base.Awake();
        }
        private void OnEnable()
        {
            InitPlayerAbility();
        }
        #endregion
    }
}
