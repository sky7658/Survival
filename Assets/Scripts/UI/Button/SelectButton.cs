using LMS.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class SelectButton : MonoBehaviour
    {
        [Header("# Reward Button Object")]
        [SerializeField] private List<Button> btns;

        [Header("# Weapon UI")]
        [SerializeField] private WeaponUI weaponUI;

        [Header("# Image UI")]
        [SerializeField] private Image backGround;

        private static readonly int rewardsCount = 2;       // 선택할 수 있는 보상 갯수
        private int[] selectIndexs = new int[rewardsCount]; // 선택한 Button에 Reward의 index값을 저장하는 배열
        private float[] startX = new float[2] { 0f, -160f };
        private int intervalX = 320;

        private List<int> indexofWeaponTypes = new List<int>();
        private readonly Dictionary<int, string> wnameofIndex = new Dictionary<int, string>() // Index의 Weapon 이름 Dictionary
        {
            { 0, "Bow" },
            { 1, "WizardBook" },
            { 2, "Ring" }
        };

        private Image[] imgs = new Image[rewardsCount];
        private Text[] texts = new Text[rewardsCount];

        private void Awake()
        {
            backGround.rectTransform.sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;

            for (int i = 0; i < rewardsCount; i++)
            {
                imgs[i] = btns[i].transform.GetChild(1).GetComponent<Image>();
                texts[i] = btns[i].transform.GetChild(3).GetComponent<Text>();

                int index = i; // 람다식 내부에 저장되는 index값이 for문의 index(int i)값으로 참조되지 않게 새로운 참조값 할당
                btns[i].onClick.AddListener(() =>
                {
                    PlayManager.Instance.WeaponLevelUp(wnameofIndex[selectIndexs[index]]); // Player에게 선택한 무기 할당

                    if (PlayManager.Instance.GetWeaponLevel(wnameofIndex[selectIndexs[index]]) == Base.WeaponInfo.MaxLevel)
                        indexofWeaponTypes.Remove(selectIndexs[index]); // 무기 레벨이 MAX일 경우 Rewards List에서 삭제

                    weaponUI.AddWeaponUI(wnameofIndex[selectIndexs[index]]); // Player가 가지고 있는 Weapon UI 업데이트

                    btns.ForEach(btn => btn.gameObject.SetActive(false));
                    gameObject.SetActive(false);
                    PlayManager.Instance.PlayGame();
                });
            }

            for (int i = 0; i < wnameofIndex.Count; i++) indexofWeaponTypes.Add(i); 
        }
        private void OnEnable()
        {
            if (indexofWeaponTypes.Count > 0)
            {
                PlayManager.Instance.PauseGame();
                SetRewards();
            } else gameObject.SetActive(false);
        }
        private List<int> InitReardsIndexs()
        {
            List<int> _rewards = new List<int>(); // 선택 가능한 Rewards를 return할 List
            List<int> _rewardsList = new List<int>(indexofWeaponTypes.ToList()); // 현재 Reward 수령이 가능한 List를 깊은 복사하여 대입

            for (int i = 0; i < rewardsCount; i++)
            {
                var _choice = Random.Range(0, _rewardsList.Count); // Rewards 중 랜덤으로 하나 선택
                _rewards.Add(_rewardsList[_choice]);
                _rewardsList.RemoveAt(_choice);                     // 선택된 Reward가 중복 선택이 되지 않게 하기 위해 list에서 삭제
                if (_rewardsList.Count == 0) break;                 // 더 이상 수령할 보상이 없다면 반복문 탈출
            }
            return _rewards;
        }
        private void SetRewards()
        {
            List<int> _list = InitReardsIndexs();

            for (int i = 0; i < _list.Count; i++)
            {
                btns[i].gameObject.SetActive(true);
                btns[i].transform.localPosition = new Vector2(startX[_list.Count - 1] + intervalX * i, 0f);
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
            var _weaponLevel = PlayManager.Instance.GetWeaponLevel(wName);
            if (_weaponLevel == -1)
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

            return GetNextLevelDescription(_infos[_weaponLevel + 1]);
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
