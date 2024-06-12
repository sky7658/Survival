using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace LMS.UI
{
    public class LoadingScene : MonoBehaviour
    {
        private static readonly List<string> nameByIndex = new List<string>() { "MainScene", "GameScene" };
        public static string GetNameByIndex(int sceneIndex)
        {
            if (sceneIndex >= nameByIndex.Count)
            {
                Debug.Log($"{sceneIndex} is Out Range in SceneIndex");
                return null;
            }
            return nameByIndex[sceneIndex];
        }
        public static void LoadScene(string sceneName)
        {
            Transform _parent = GameObject.Find("Canvas").transform;

            var _loading = Instantiate(Manager.ResourceManager.Instance.GetObject<LoadingScene>("LoadingImage"));
            _loading.transform.SetParent(_parent, false);
            _loading.Load(sceneName);
        }
        public static void LoadScene(int sceneIndex) => LoadScene(GetNameByIndex(sceneIndex));

        [Header("# Loading UI")]
        [SerializeField] private Image loadingBar;
        [SerializeField] private Text percentText;

        private readonly string percentString = "Loading...{0}%";

        private Coroutine coroutine;

        private void Awake()
        {
            loadingBar = transform.GetChild(0).GetComponent<Image>();
            percentText = transform.GetChild(1).GetComponent<Text>();
        }
        private void OnEnable()
        {
            loadingBar.fillAmount = 0f;
        }
        private void Load(string sceneName)
        {
            Manager.CoroutineManager.Instance.StopAllCoroutines();
            coroutine = Manager.CoroutineManager.Instance.ExecuteCoroutine(Loading(sceneName));
        }
        private IEnumerator Loading(string sceneName)
        {
            var _op = SceneManager.LoadSceneAsync(sceneName);
            float _elapsed = 0f;

            _op.allowSceneActivation = false;
            while (!_op.isDone)
            {
                _elapsed += Time.unscaledDeltaTime;
                if (_op.progress < 0.9f)
                {
                    loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, _op.progress, _elapsed);
                    if (loadingBar.fillAmount >= _op.progress) _elapsed = 0f;
                }
                else loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, _elapsed);

                percentText.text = string.Format(percentString, Mathf.FloorToInt(loadingBar.fillAmount * 100));

                if (loadingBar.fillAmount >= 1.0f) break;
                yield return null;
            }
            _op.allowSceneActivation = true;
            yield break;
        }
    }
}