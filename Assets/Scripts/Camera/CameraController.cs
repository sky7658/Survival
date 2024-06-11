using System.Collections;
using UnityEngine;
using System;
using LMS.Utility;

namespace LMS.User
{
    public class CameraController : MonoBehaviour
    {
        private Camera mCamera;
        private CoroutineController cc;

        private Vector3 orginPos = new Vector3(0f, 0f, -10f);
        private float zoomInSize = 2f;
        private float zoomOutSize = 5f;

        private float velocityF = 0f;
        private Vector3 velocity = Vector3.zero;

        public void ShowTarget(Vector2 pos, float stayTime, Action action = null) => cc.ExecuteCoroutine(Move(pos, stayTime, action), "ShowTarget");
        private IEnumerator Move(Vector2 pos, float stayTime, Action action)
        {
            float _elapsed = 0f;
            float _smoothTime = 0.2f;

            CameraZoomInOut(stayTime);

            while (_elapsed < 1f)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(pos.x, pos.y, -10f), ref velocity, _smoothTime);
                _elapsed += Time.deltaTime;
                yield return null;
            }

            _elapsed = 0f;
            yield return UtilFunctions.WaitForSeconds(stayTime);
            while (_elapsed < 1f)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, orginPos, ref velocity, _smoothTime);
                _elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = orginPos;
            if(action != null) action();
            yield break;
        }

        public void CameraZoomInOut(float stayTime, Action action = null) => cc.ExecuteCoroutine(ZoomInOut(stayTime, action), "ZoomInOut");
        private IEnumerator ZoomInOut(float stayTime, Action action)
        {
            float _elapsed = 0f;
            float _smoothTime = 0.2f;

            while (_elapsed < 1f)
            {
                mCamera.orthographicSize = Mathf.SmoothDamp(mCamera.orthographicSize, zoomInSize, ref velocityF, _smoothTime);
                _elapsed += Time.deltaTime;
                yield return null;
            }

            _elapsed = 0f;
            yield return UtilFunctions.WaitForSeconds(stayTime);

            while (_elapsed < 1f)
            {
                mCamera.orthographicSize = Mathf.SmoothDamp(mCamera.orthographicSize, zoomOutSize, ref velocityF, _smoothTime);
                _elapsed += Time.deltaTime;
                yield return null;
            }

            mCamera.orthographicSize = zoomOutSize;
            if (action != null) action();
            yield break;
        }
        
        public void ShakeCamera(float durateTime) => cc.ExecuteCoroutine(ShakeObject(durateTime), "Shake");
        private IEnumerator ShakeObject(float durateTime)
        {
            float _elapsed = 0f;

            while (_elapsed < durateTime)
            {
                var _posX = UnityEngine.Random.Range(-0.2f, 0.2f);
                var _posY = UnityEngine.Random.Range(-0.2f, 0.2f);

                transform.localPosition = new Vector3(_posX, _posY, -10f);
                _elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = orginPos;
            yield break;
        }
        private void SetScreenSize()
        {
            float targetWidthAspect = 16.0f;

            //세로 화면 비율
            float targetHeightAspect = 9.0f;

            //메인 카메라
            Camera mainCamera = Camera.main;

            mainCamera.aspect = targetWidthAspect / targetHeightAspect;

            float widthRatio = (float)Screen.width / targetWidthAspect;
            float heightRatio = (float)Screen.height / targetHeightAspect;

            float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
            float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

            // 16_10비율보다 가로가 짦다면(4_3 비율)
            // 16_10비율보다 세로가 짧다면(16_9 비율)
            // 시작 지점을 0으로 만들어준다
            if (heightRatio > widthRatio)
                widthtadd = 0.0f;
            else
                heightadd = 0.0f;


            mainCamera.rect = new Rect(
                mainCamera.rect.x + Math.Abs(widthtadd),
                mainCamera.rect.y + Math.Abs(heightadd),
                mainCamera.rect.width + (widthtadd * 2),
                mainCamera.rect.height + (heightadd * 2));
        }
        private void Awake()
        {
            mCamera = GetComponent<Camera>();
            SetScreenSize();
        }
        void Start()
        {
            cc = new CoroutineController();

            cc.AddCoroutine("Shake");
            cc.AddCoroutine("ZoomInOut");
            cc.AddCoroutine("ShowTarget");
        }
    }

}
