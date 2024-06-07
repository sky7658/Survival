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
            startBtn.onClick.AddListener(() => LoadingScene.LoadScene(1)); // 이거 수정할거임
            ShopBtn.onClick.AddListener(() => shopUI.SetActive(true));
            OptionBtn.onClick.AddListener(() => optionUI.SetActive(true));
        }
    }
}
