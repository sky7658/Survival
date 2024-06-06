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
            mainBtn.onClick.AddListener(() => SceneManager.LoadScene(0)); // �̰� �����Ұ���
        }
    }
}
