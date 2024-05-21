using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LMS.Manager;

namespace LMS.UI
{
    public class GameTime : MonoBehaviour
    {
        private Text timeText;
        private Coroutine coroutine;
        private void Awake()
        {
            timeText = GetComponent<Text>();
            coroutine = CoroutineManager.Instance.ExecuteCoroutine(UpdateTime());
        }
        private IEnumerator UpdateTime()
        {
            string _min = "";
            string _sec = "";
            while (true)
            {
                var gameTime = PlayManager.Instance.ElapsedTime += Time.deltaTime;

                if (gameTime % 60 < 10)
                    _sec = string.Format("0{0}", (int)gameTime % 60);
                else
                    _sec = string.Format("{0}", (int)gameTime % 60);

                if (gameTime / 60 < 10)
                    _min = string.Format("0{0}", (int)gameTime / 60);
                else
                    _min = string.Format("{0}", (int)gameTime / 60);

                timeText.text = _min + " : " + _sec;
                yield return null;
            }
        }
    }
}
