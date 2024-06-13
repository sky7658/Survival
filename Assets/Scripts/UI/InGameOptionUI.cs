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
            mainBtn.onClick.AddListener(() => ButtonEvent.ButtonClickEvent(() => LoadingScene.LoadScene(0)));
        }
        protected override void ExitButtonEvent() => ButtonEvent.ButtonClickEvent(() => PlayerInterface.Instance.ActiveOptionUI());
    }
}
