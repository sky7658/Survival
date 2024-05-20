using LMS.Enemy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class SelectButton : MonoBehaviour
    {
        private static readonly int rewardsCount = 2;

        private List<List<int>> rewardsIndex = new List<List<int>>();
        private readonly Dictionary<int, string> wnameofIndex = new Dictionary<int, string>()
        {
            { 0, "Bow" },
            { 1, "WizardBook" },
            { 2, "Ring" }
        };

        private Image[] imgs = new Image[rewardsCount];
        private Text[] texts = new Text[rewardsCount];

        private void Awake()
        {
            for (int i = 0; i < rewardsCount; i++)
            {
                imgs[i] = transform.GetChild(i).transform.GetChild(1).GetComponent<Image>();
                texts[i] = transform.GetChild(i).transform.GetChild(3).GetComponent<Text>();
            }
            InitRewardsIndex(new List<int>(), new bool[wnameofIndex.Count]);
        }
        private void OnEnable()
        {
            SetRewards();
        }
        private void InitRewardsIndex(List<int> ints, bool[] visit, int n = 0)
        {
            if (ints.Count == rewardsCount)
            {
                rewardsIndex.Add(new List<int>(ints.ToList()));
                return;
            }
            for (int i = 0; i < wnameofIndex.Count; i++)
            {
                if ((n == i && ints.Count > 0) || visit[i]) continue;
                ints.Add(i);
                visit[i] = true;
                InitRewardsIndex(ints, visit, i);
                visit[i] = false;
                ints.RemoveAt(ints.Count - 1);
            }
        }
        public void SetRewards()
        {
            var _list = rewardsIndex[Random.Range(0, rewardsIndex.Count)];

            for (int i = 0; i < rewardsCount; i++)
            {
                imgs[i].sprite = SetImage(_list[i]);
            }
        }

        private Sprite SetImage(int index)
        {
            if (!wnameofIndex.TryGetValue(index, out var _name)) Debug.Log($"{index} is not exist in wnameofIndex.Key");
            return Manager.ResourceManager.Instance.GetSprite(_name);
        }
        private string SetText(int index)
        {
            return "";
        }
        public void SelectReward()
        {
        }

        private void Update()
        {
            
        }
    }
}
