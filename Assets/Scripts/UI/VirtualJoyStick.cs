using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LMS.UI
{
    public class VirtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform joyStickObject;
        [SerializeField] private RectTransform handle;
        [SerializeField] private RectTransform handleBackGround;
        private Vector2 startPointer;
        private void Awake()
        {
            handleBackGround = Instantiate(joyStickObject);
            handleBackGround.SetParent(transform, false);

            handle = handleBackGround.transform.GetChild(0).GetComponent<RectTransform>();
            handleBackGround.gameObject.SetActive(false);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            handleBackGround.gameObject.SetActive(true);
            handleBackGround.anchoredPosition = eventData.position;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            startPointer = eventData.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            handle.anchoredPosition = eventData.position - startPointer;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            handle.anchoredPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
