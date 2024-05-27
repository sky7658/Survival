using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace LMS.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private Text amount;
        [SerializeField] private Image img;
        public void UpdateMoney(int value)
        {
            img.sprite = Coin.GetMoneySprite(value);
            amount.text = $"{value}G";
        }
    }
}
