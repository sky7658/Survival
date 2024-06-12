using LMS.Enemy;
using LMS.Manager;
using LMS.Utility;
using UnityEngine;

namespace LMS.UI
{
    public class PlayerInterface : MonoSingleton<PlayerInterface>
    {
        [Header("# Play UI")]
        [SerializeField] private GameObject rewardsButton;
        [SerializeField] private GaugeBar expBar;
        [SerializeField] private UnityEngine.UI.Text playerLevelText;
        [SerializeField] private MoneyUI moneyUI;
        [SerializeField] private UnityEngine.UI.Button optionButton;
        [SerializeField] private GameObject optionUI;

        public void ActiveRewardUI() => rewardsButton.SetActive(true);
        public void ActiveOptionUI()
        {
            if (optionUI.activeSelf)
            {
                optionUI.SetActive(false);
                if (!rewardsButton.activeSelf) PlayManager.Instance.PlayGame();
            }
            else
            {
                optionUI.SetActive(true);
                PlayManager.Instance.PauseGame();
            }
        }
        int fps = 0;
        float time = 0f;
        public UnityEngine.UI.Text t;
        public void UpdateLevelText(int value)
        {
            fps++;
            time += Time.unscaledDeltaTime;
            if (time > 1f)
            {
                t.text = $"FPS : {fps}";
                fps = 0;
                time = 0f;
            }
            //playerLevelText.text = $"Lv : {value}";
            playerLevelText.text = $"Lv : {value}\n¸ó½ºÅÍ °¹¼ö : {CommonMonster.aliveMonsterCount}\nTime Scale : {Time.timeScale}";
        }
        public void UpdateMoneyUI(int value) => moneyUI.UpdateMoney(value);
        public void UpdateExpBar(float value) => expBar.UpdateGaugeBar(value);
        public void SetMaxGaugeValue(float value) => expBar.SetMaxGaugeValue(value);

        protected override void Awake()
        {
            base.Awake();
            optionButton.onClick.AddListener(() => ActiveOptionUI());
            moneyUI.UpdateMoney(0);
        }

        private void Start()
        {
            expBar.Initialized(PlayManager.Instance.MaxExp);
        }
    }
}
