using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LMS.UI
{
    public class InGameOptionUI : OptionUI
    {
        [SerializeField] private Button mainBtn;

        protected override void Awake()
        {
            base.Awake();
            mainBtn.onClick.AddListener(() => LoadingScene.LoadScene(0)); // 이거 수정할거임
        }
    }
}
