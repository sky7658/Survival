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

        [Header("# ShopUI")]
        [SerializeField] private GameObject shopUI;

        private void Awake()
        {
            ShopBtn.onClick.AddListener(() => shopUI.SetActive(true));
        }
    }
}
