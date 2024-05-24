using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace LMS.UI
{
    public class DamageText : MonoBehaviour
    {
        private Text damageText;
        private void Awake()
        {
            damageText = transform.GetChild(0).GetComponent<Text>();
        }
        public void Initialized(float value, Vector2 pos)
        {
            transform.localScale = Vector3.one;
            damageText.text = value.ToString();
            damageText.transform.position = pos;
        }
        public void ReturnObject() => Utility.ObjectPool.Instance.ReturnObject(this, "DamageText");
    }
}
