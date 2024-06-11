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

        private static readonly int rewardsCount = 2;       // ������ �� �ִ� ���� ����
        private int[] selectIndexs = new int[rewardsCount]; // ������ Button�� Reward�� index���� �����ϴ� �迭
        private float[] startX = new float[2] { 0f, -160f };
        private int intervalX = 320;

        private List<int> indexofWeaponTypes = new List<int>();
        private readonly Dictionary<int, string> wnameofIndex = new Dictionary<int, string>() // Index�� Weapon �̸� Dictionary
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

                int index = i; // ���ٽ� ���ο� ����Ǵ� index���� for���� index(int i)������ �������� �ʰ� ���ο� ������ �Ҵ�
                btns[i].onClick.AddListener(() =>
                {
                    PlayManager.Instance.WeaponLevelUp(wnameofIndex[selectIndexs[index]]); // Player���� ������ ���� �Ҵ�

                    if (PlayManager.Instance.GetWeaponLevel(wnameofIndex[selectIndexs[index]]) == Base.WeaponInfo.MaxLevel)
                        indexofWeaponTypes.Remove(selectIndexs[index]); // ���� ������ MAX�� ��� Rewards List���� ����

                    weaponUI.AddWeaponUI(wnameofIndex[selectIndexs[index]]); // Player�� ������ �ִ� Weapon UI ������Ʈ

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
            List<int> _rewards = new List<int>(); // ���� ������ Rewards�� return�� List
            List<int> _rewardsList = new List<int>(indexofWeaponTypes.ToList()); // ���� Reward ������ ������ List�� ���� �����Ͽ� ����

            for (int i = 0; i < rewardsCount; i++)
            {
                var _choice = Random.Range(0, _rewardsList.Count); // Rewards �� �������� �ϳ� ����
                _rewards.Add(_rewardsList[_choice]);
                _rewardsList.RemoveAt(_choice);                     // ���õ� Reward�� �ߺ� ������ ���� �ʰ� �ϱ� ���� list���� ����
                if (_rewardsList.Count == 0) break;                 // �� �̻� ������ ������ ���ٸ� �ݺ��� Ż��
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
                _text += " ���ݷ� ����";
            }
            if (infos.wInfo.penetrationCnt > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "���� �� ����";
            }
            if (infos.wInfo.speed > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "������Ʈ ���ǵ� ����";
            }
            if (infos.coolTime > 0f)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "��Ÿ�� ����";
            }
            if (infos.objectCount > 0)
            {
                if (_text.Length > 0) _text += ", ";
                _text += "������Ʈ �� ����";
            }
            return _text;
        }
    }
}
