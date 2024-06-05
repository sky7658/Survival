using LMS.Manager;
using System.Collections.Generic;
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
        private readonly string abilityDescription = "�ɷ�ġ {0}% ���";

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
            myMoney.text = string.Format(moneyText, GameManager.Instance.GetPlayerMoney);
        }
        private void UpgradeAbility(int index)
        {
            if (GameManager.Instance.GetAbilityLevel(nameofIndex[index]) == User.Ability.abilityMaxLevel - 1) return;
            if (GameManager.Instance.TryUpgradeAbility(nameofIndex[index]))
            {
                UpdateAbilityInfo(index);
            }
        }
        private void UpdateAbilityInfo(int index)
        {
            var _gmr = GameManager.Instance;
            myMoney.text = string.Format(moneyText, _gmr.GetPlayerMoney);

            var _name = nameofIndex[index];
            var _level = _gmr.GetAbilityLevel(_name);

            var _nextRatio = _gmr.GetAbilityNextRatio(_name);
            if (_nextRatio == default(float))
            {
                descriptions[index].text = "Max Level";
                prices[index].text = "-";
            }
            else
            {
                descriptions[index].text = string.Format(abilityDescription, _nextRatio * 100);
                prices[index].text = string.Format(moneyText, _gmr.GetAbilityNextPrice(_name));
            }

            if (_level < 0) return;
            for (int i = 0; i <= _level; i++)
                abilities[index].transform.GetChild(2).transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}