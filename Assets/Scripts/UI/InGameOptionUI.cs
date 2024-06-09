using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class InGameOptionUI : OptionUI
    {
        [SerializeField] private Button mainBtn;

        protected override void Awake()
        {
            base.Awake();
            mainBtn.onClick.AddListener(() => LoadingScene.LoadScene(0));
            exitBtn.onClick.AddListener(() => Manager.PlayManager.Instance.PlayGame());
        }
    }
}
