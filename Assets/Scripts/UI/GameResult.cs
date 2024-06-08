using LMS.Manager;
using LMS.User;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class GameResult : MonoBehaviour
    {
        public static void CreateGameOverResult() => CreateResult("Game Over");
        public static void CreateGameClearResult() => CreateResult("Game Clear");
        private static void CreateResult(string resultText)
        {
            Transform _parent = GameObject.Find("Canvas").transform;

            var _result = Instantiate(ResourceManager.Instance.GetObject<GameResult>("Result"));
            _result.transform.SetParent(_parent, false);
            _result.Initialized(resultText);
        }

        [SerializeField] private Text gameResultText;
        [SerializeField] private Button mainBtn;
        [SerializeField] private Text moneyText;

        private readonly float intervalY = 40f;
        private readonly string gameResultString = "Result({0})";
        private readonly string damageText = "{0}";
        private readonly static char[] damageUnits = new char[3] { ' ', 'K', 'M' };

        private void Awake()
        {
            gameResultText = transform.GetChild(0).GetComponent<Text>();
            mainBtn = transform.GetChild(1).GetComponent<Button>();
            moneyText = transform.GetChild(2).GetComponent<Text>();
        }
        private void OnEnable()
        {
            // ¹öÆ° ÀÌº¥Æ® Ãß°¡
            mainBtn.onClick.AddListener(() => LoadingScene.LoadScene(0));

            // È¹µæ Money ´ëÀÔ
            var _getMoney = PlayManager.Instance.GetMyMoney;
            moneyText.text = string.Format(MoneyUI.moneyString + " È¹µæ!", _getMoney);
            moneyText.transform.GetChild(0).GetComponent<Image>().sprite = Coin.GetMoneySprite(_getMoney);
            GameManager.Instance.UpdateGameData(_getMoney);

            // ´©Àû µ¥¹ÌÁö Text »ý¼º
            int _index = 0;
            for (int i = 0; i < 3; i++)
            {
                string _imgName = "";
                float _value = 0f;
                switch (i)
                {
                    case 0:
                        _imgName = Base.WeaponInfo.wnameSO.Bow;
                        _value = Arrow.CumulativeDamage;
                        break;
                    case 1:
                        _imgName = Base.WeaponInfo.wnameSO.WizardBook;
                        _value = Sword.CumulativeDamage;
                        break;
                    case 2:
                        _imgName = Base.WeaponInfo.wnameSO.Ring;
                        _value = GemStone.CumulativeDamage;
                        break;
                }
                if (!PlayManager.Instance.IsExistWeapon(_imgName)) continue;

                string _damage = "";
                for (int j = 0; j < damageUnits.Length; j++)
                {
                    if (_value / 1000 < 1)
                    {
                        _damage = (Mathf.Round(_value * 10) * 0.1f).ToString() + damageUnits[j];
                        break;
                    }
                    _value /= 1000f;
                }

                var _text = Instantiate(ResourceManager.Instance.GetObject<Text>("DamageResultText"));
                _text.transform.SetParent(transform, false);
                _text.transform.localPosition = new Vector2(-260f, 110f - intervalY * _index++);

                _text.transform.GetChild(0).GetComponent<Image>().sprite = ResourceManager.Instance.GetSprite(_imgName);
                _text.text = string.Format(damageText, _damage);

            }
        }

        public void Initialized(string resultText) => gameResultText.text = string.Format(gameResultString, resultText);
    }
}
