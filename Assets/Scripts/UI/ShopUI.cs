using LMS.Manager;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class ShopUI : MonoBehaviour
    {
        [Header("# Abilities")]
        [SerializeField] private List<Image> abilities = new List<Image>();

        [Header("# Button UI")]
        [SerializeField] private List<Button> upgradeBtns = new List<Button>();
        [SerializeField] private Button exitBtn;

        [Header("# Text")]
        [SerializeField] private List<Text> descriptions = new List<Text>();
        [SerializeField] private List<Text> prices = new List<Text>();
        [SerializeField] private Text myMoney;

        private List<string> nameofIndex = new List<string>() { "Shoes", "Hat", "Candy" };
        private int abilityCount;
        private readonly string moneyText = "{0}G";
        private readonly string abilityDescription = "NextLevel : ´É·ÂÄ¡ {0}% »ó½Â";

        private void Awake()
        {
            abilityCount = transform.GetChild(0).transform.childCount;
            for (int i = 0; i < abilityCount; i++)
            {
                int index = i;

                abilities.Add(transform.GetChild(0).transform.GetChild(i).GetComponent<Image>());
                upgradeBtns.Add(abilities[i].transform.GetChild(1).GetComponent<Button>());
                descriptions.Add(abilities[i].transform.GetChild(3).GetComponent<Text>());
                prices.Add(abilities[i].transform.GetChild(4).GetComponent<Text>());

                upgradeBtns[i].onClick.AddListener(() => UpgradeAbility(index));
            }

            exitBtn.onClick.AddListener(() => ButtonEvent.UIExitEvent(gameObject));

            for (int i = 0; i < abilityCount; i++) UpdateAbilityInfo(i);
        }

        private void OnEnable()
        {
            myMoney.text = string.Format(moneyText, GameManager.Instance.PlayerMoney);
        }
        private void UpgradeAbility(int index)
        {
            if (ShopManager.Instance.GetAbilityLevel(nameofIndex[index]) == User.Ability.abilityMaxLevel - 1) return;
            if (ShopManager.Instance.TryUpgradeAbility(nameofIndex[index]))
            {
                UpdateAbilityInfo(index);
            }
        }
        private void UpdateAbilityInfo(int index)
        {
            var _smr = ShopManager.Instance;
            myMoney.text = string.Format(moneyText, GameManager.Instance.PlayerMoney);

            var _name = nameofIndex[index];
            var _level = _smr.GetAbilityLevel(_name);

            var _nextRatio = _smr.GetAbilityNextRatio(_name);
            if (_nextRatio == default(float))
            {
                descriptions[index].text = "Max Level";
                prices[index].text = "-";
            }
            else
            {
                descriptions[index].text = string.Format(abilityDescription, _nextRatio * 100);
                prices[index].text = string.Format(moneyText, _smr.GetAbilityNextPrice(_name));
            }

            UpdatePriceTextColor();

            if (_level < 0) return;
            for (int i = 0; i <= _level; i++)
                abilities[index].transform.GetChild(2).transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }
        private void UpdatePriceTextColor()
        {
            for (int i = 0; i < abilityCount; i++)
            {
                var _name = nameofIndex[i];
                var _nextPrice = ShopManager.Instance.GetAbilityNextPrice(_name);

                if (_nextPrice <= GameManager.Instance.PlayerMoney || _nextPrice.Equals(default(int))) prices[i].color = Color.white;
                else prices[i].color = Color.red;
            }
        }
    }
}
