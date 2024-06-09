using LMS.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Manager
{
    public class ShopManager : MonoSingleton<ShopManager>
    {
        public int GetAbilityLevel(string name) => GameManager.Instance.TryGetAbility(name).GetLevel;
        public int GetAbilityNextPrice(string name) => GameManager.Instance.TryGetAbility(name).GetPriceByNextLevel();
        public float GetAbilityNextRatio(string name) => GameManager.Instance.TryGetAbility(name).GetRatioByNextLevel();
        public bool TryUpgradeAbility(string name)
        {
            var _price = GetAbilityNextPrice(name);
            if (GameManager.Instance.PlayerMoney < _price)
            {
                Debug.Log("µ· ºÎÁ·");
                return false;
            }
            GameManager.Instance.TryGetAbility(name).UpgradeAbility();
            GameManager.Instance.PlayerMoney -= _price;
            return true;
        }
    }
}
