using UnityEngine;
using UnityEngine.EventSystems;

namespace LMS.UI
{
    public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public static readonly string JoyStickName = "JoyStick";

        [SerializeField] private RectTransform handle;
        [SerializeField] private RectTransform handleBackGround;
        [SerializeField] private RectTransform parent;
        private readonly float range = 50f;

        private Vector2 inputVector = Vector2.zero;
        public Vector2 InputVector { get { return inputVector; } }
        private void Awake()
        {
            parent = GetComponent<RectTransform>();
            handleBackGround.gameObject.SetActive(false);
        }
        private void Start()
        {
            parent.sizeDelta = parent.transform.parent.GetComponent<RectTransform>().sizeDelta;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            handleBackGround.gameObject.SetActive(true);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle
                (parent, eventData.position, eventData.pressEventCamera, out var _localPoint))
            {
                handleBackGround.anchoredPosition = _localPoint;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle
                (handleBackGround, eventData.position, eventData.pressEventCamera, out var _localPoint))
            {
                var _dis = Mathf.Clamp(_localPoint.magnitude, 0, range);
                var _dir = _localPoint.normalized;

                handle.anchoredPosition = _dir * _dis;
                inputVector = _localPoint.normalized * _dis / range;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            handle.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            handleBackGround.gameObject.SetActive(false);
        }
    }
}
