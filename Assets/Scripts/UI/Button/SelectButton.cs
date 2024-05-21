using LMS.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace LMS.UI
{
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private List<Button> btns;

        private static readonly int rewardsCount = 2;
        private int[] selectIndexs = new int[rewardsCount];

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

                int index = i;
                btns[i].onClick.AddListener(() =>
                {
                    PlayManager.Instance.WeaponLevelUp(wnameofIndex[selectIndexs[index]]);
                    Time.timeScale = 1f;
                    gameObject.SetActive(false);
                });
            }
            InitRewardsIndex(new List<int>(), new bool[wnameofIndex.Count]);
        }
        private void OnEnable()
        {
            Time.timeScale = 0f;
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
        private void SetRewards()
        {
            var _list = rewardsIndex[Random.Range(0, rewardsIndex.Count)];

            for (int i = 0; i < rewardsCount; i++)
            {
                selectIndexs[i] = _list[i];
                if (!wnameofIndex.TryGetValue(_list[i], out var _name))
                {
                    Debug.Log($"{_list[i]} is not exist in wnameofIndex.Key");
                    _name = "NULL";
                }
                imgs[i].sprite = SetImage(_name);
                texts[i].text = SetText(_name);
            }
        }
        private Sprite SetImage(string wName)
        {
            return ResourceManager.Instance.GetSprite(wName);
        }
        private string SetText(string wName)
        {
            var _nextLevel = PlayManager.Instance.GetWeaponLevel(wName) + 2;
            if (_nextLevel == 1)
            {
                if (!Base.WeaponInfo.WeaponDescriptions.TryGetValue(wName, out var _text))
                {
                    Debug.Log($"{wName} is not Exist in WeaponName");
                    return "";
                }
                return _text;
            }
            if (!Base.WeaponInfo.wlevelInfo.TryGetValue(wName, out var _infos))
            {
                Debug.Log($"{wName} is not Exist in WeaponName");
                return "";
            }

            return GetNextLevelDescription(_infos[_nextLevel]);
        }
        private string GetNextLevelDescription(General.WeaponInfos infos)
        {
            var _text = "";
            if (infos.wInfo.atk > 0f)
            {
                _text += " 공격력 증가";
            }
            if (infos.wInfo.penetrationCnt > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "적을 더 관통";
            }
            if (infos.wInfo.speed > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "오브젝트 스피드 증가";
            }
            if (infos.coolTime > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "쿨타임 감소";
            }
            if (infos.objectCount > 0)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "오브젝트 수 증가";
            }
            return _text;
        }
    }
}
