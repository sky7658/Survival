using UnityEngine;

namespace LMS.Controller
{
    public class TouchHandler : InputBase
    {
        public bool isClick => Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began);
        public Vector2 clickPos => Input.GetTouch(0).position;
        public float wheelMount => WheelAmount();
        private UI.VirtualJoyStick joyStick;
        private UI.VirtualJoyStick JoyStick
        {
            get
            {
                if (joyStick == null)
                {
                    var _prefab = Manager.ResourceManager.GetObject<UI.VirtualJoyStick>(UI.VirtualJoyStick.JoyStickName);
                    joyStick = GameObject.Instantiate(_prefab);
                    joyStick.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    joyStick.transform.SetAsFirstSibling();
                }
                return joyStick;
            }
        }
        public float x { get{ return JoyStick.InputVector.x; } }
        public float y { get { return JoyStick.InputVector.y; } }

        private float WheelAmount()
        {
            return default(float);
        }
    }
}
