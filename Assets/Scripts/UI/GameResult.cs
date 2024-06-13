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

            var _result = Instantiate(ResourceManager.GetObject<GameResult>("Result"));
            _result.transform.SetParent(_parent, false);
            _result.Initialized(resultText, resultText == "Game Clear" ? 1 : 0);
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
        public void Initialized(string resultText, int count)
        {
            PlayManager.Instance.PauseGame();

            // 버튼 이벤트 추가
            mainBtn.onClick.AddListener(() => ButtonEvent.ButtonClickEvent(() => LoadingScene.LoadScene(0)));

            // 획득 Money 대입
            var _getMoney = PlayManager.Instance.GetMyMoney;
            moneyText.text = string.Format(MoneyUI.moneyString + " 획득!", _getMoney);
            moneyText.transform.GetChild(0).GetComponent<Image>().sprite = Coin.GetMoneySprite(_getMoney);

            // GameData Update
            GameManager.Instance.UpdateGameData(_getMoney, count);

            // 누적 데미지 Text 생성
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

                var _text = Instantiate(ResourceManager.GetObject<Text>("DamageResultText"));
                _text.transform.SetParent(transform, false);
                _text.transform.localPosition = new Vector2(0f, 110f - intervalY * _index++);

                _text.transform.GetChild(0).GetComponent<Image>().sprite = ResourceManager.GetSprite(_imgName);
                _text.text = string.Format(damageText, _damage);
            }

            // 게임 결과 Text
            gameResultText.text = string.Format(gameResultString, resultText);
        }
    }
}
