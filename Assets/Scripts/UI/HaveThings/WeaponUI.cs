using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LMS.Manager;
using Unity.VisualScripting;

namespace LMS.UI
{
    public class WeaponUI : MonoBehaviour
    {
        private Dictionary<string, Transform> weaponBoxes = new Dictionary<string, Transform>();

        [SerializeField] private float speed;

        private float currentPosY = -150f;
        private float intervalY = 100f;
        private Vector3 velocity = Vector3.zero;

        private Coroutine coroutine;

        public void AddWeaponUI(string name)
        {
            var _weaponLevel = PlayManager.Instance.GetWeaponLevel(name);
            if (_weaponLevel == 1) // ���ο� ���Ⱑ �߰��� ����
            {
                Image _img = transform.GetChild(weaponBoxes.Count).transform.GetChild(0).GetComponent<Image>();
                _img.sprite = ResourceManager.GetSprite(name);

                weaponBoxes.Add(name, _img.transform); // ���� ������ �ִ� Weapon�� UI ������ �߰�
                coroutine = Utility.UtilCoroutine.ExecuteCoroutine(UpdateUI(), coroutine); // ���ο� ���� �߰��� ���� UI �ִϸ��̼�
                return;
            }

            if (!weaponBoxes.TryGetValue(name, out var _weaponBox))
            {
                Debug.Log($"{name} is not exist in WeaponNames");
                return;
            }

            _weaponBox.GetChild(_weaponLevel - 1).GetChild(0).gameObject.SetActive(true);
        }

        private IEnumerator UpdateUI()
        {
            Vector3 _targetPos = new Vector3(transform.localPosition.x, currentPosY += intervalY, 0f);
            float _elapsed = 0f;
            while (_elapsed < 1f)
            {
                _elapsed += Time.deltaTime;
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _targetPos, ref velocity, speed);
                yield return null;
            }
            transform.localPosition = _targetPos;
            yield break;
        }
    }
}
