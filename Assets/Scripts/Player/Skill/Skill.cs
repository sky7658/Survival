using UnityEngine;
using UnityEngine.UI;

namespace LMS.User
{
    public abstract class Skill : MonoBehaviour
    {
        [SerializeField] private Image coolTimeImg;
        [SerializeField] private float coolTime;
        [SerializeField] private float elapsedTime;
        protected void Initialized(float coolTime)
        {
            this.coolTime = coolTime;
            elapsedTime = coolTime;
        }
        protected abstract void Execute();
        private void Awake()
        {
            coolTimeImg = transform.GetChild(1).GetComponent<Image>();
        }
        private void Update()
        {
            if (elapsedTime <= 0f)
            {
                elapsedTime = coolTime;
                Execute();
            }
            elapsedTime -= Time.deltaTime;
            coolTimeImg.fillAmount = elapsedTime / coolTime;
        }
    }

}
