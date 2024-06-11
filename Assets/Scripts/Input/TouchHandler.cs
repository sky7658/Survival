using UnityEngine;
using UnityEngine.EventSystems;

namespace LMS.Controller
{
    public class TouchHandler : InputBase
    {
        public bool isClick => Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began);
        public Vector2 clickPos => Input.GetTouch(0).position;
        public float wheelMount => Input.GetAxis("Mouse ScrollWheel");
        private UI.VirtualJoyStick joyStick;
        public TouchHandler()
        {
            var _prefab = Manager.ResourceManager.Instance.GetObject<UI.VirtualJoyStick>(UI.VirtualJoyStick.JoyStickName);
            joyStick = GameObject.Instantiate(_prefab).GetComponent<UI.VirtualJoyStick>();
            joyStick.transform.SetParent(GameObject.Find("Canvas").transform, false);
            joyStick.transform.SetAsFirstSibling();
        }
        public float x { get{ return joyStick.InputVector.x; } }
        public float y { get { return joyStick.InputVector.y; } }
    }
}
