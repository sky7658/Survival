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
            if (value == 0)
            {
                damageText.text = "¸·À½";
                damageText.color = Color.white;
            }
            else
            {
                damageText.text = Mathf.FloorToInt(value).ToString();
                damageText.color = new Color(255f, 164f, 0f, 1f);
            }
            transform.position = pos;
        }
        public void ReturnObject() => Utility.ObjectPool.Instance.ReturnObject(this, "DamageText");
    }
}
