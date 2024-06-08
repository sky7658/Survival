using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace LMS.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private Text amount;
        [SerializeField] private Image img;

        public static readonly string moneyString = "{0}G";
        public void UpdateMoney(int value)
        {
            img.sprite = Coin.GetMoneySprite(value);
            amount.text = string.Format(moneyString, value);
        }
    }
}
