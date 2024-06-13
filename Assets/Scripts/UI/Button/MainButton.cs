using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class MainButton : MonoBehaviour
    {
        [Header("# Button")]
        [SerializeField] private Button startBtn;
        [SerializeField] private Button ShopBtn;
        [SerializeField] private Button OptionBtn;
        [SerializeField] private Button gameExitBtn;

        [Header("# UIObject")]
        [SerializeField] private GameObject shopUI;
        [SerializeField] private GameObject optionUI;

        private void Awake()
        {
            startBtn.onClick.AddListener(() => ButtonEvent.ButtonClickEvent(() => LoadingScene.LoadScene(1)));
            ShopBtn.onClick.AddListener(() => ButtonEvent.ButtonClickEvent(() => shopUI.SetActive(true)));
            OptionBtn.onClick.AddListener(() => ButtonEvent.ButtonClickEvent(() => optionUI.SetActive(true)));
            gameExitBtn.onClick.AddListener(() => ButtonEvent.GameExitEvent());
        }
    }
}
