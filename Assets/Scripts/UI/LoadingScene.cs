using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace LMS.UI
{
    public class LoadingScene : MonoBehaviour
    {
        [Header("# Loading UI")]
        [SerializeField] private Image loadingBar;
        [SerializeField] private Text percentText;

        private readonly string percentString = "Loading...{0}%";

        public void LoadScene(string sceneName)
        {
            Manager.CoroutineManager.Instance.ExecuteCoroutine(Loading(sceneName));
        }

        public void LoadScene(int sceneIndex)
        {
            var _scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
            Manager.CoroutineManager.Instance.ExecuteCoroutine(Loading(_scene.name));
        }

        private IEnumerator Loading(string sceneName)
        {
            var _op = SceneManager.LoadSceneAsync(sceneName);
            float _elapsed = 0f;

            _op.allowSceneActivation = false;

            while (!_op.isDone)
            {
                _elapsed += Time.deltaTime;
                if (_op.progress < 0.9f)
                {
                    loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, _op.progress, _elapsed);
                    if (loadingBar.fillAmount >= _op.progress) _elapsed = 0f;
                }
                else loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, _elapsed);

                percentText.text = string.Format(percentString, loadingBar.fillAmount);

                if (loadingBar.fillAmount >= 1.0f) yield break;
                yield return null;
            }
            _op.allowSceneActivation = true;
        }
    }
}