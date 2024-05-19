using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        
        public void ShowTarget(Vector2 pos, float stayTime) => cc.ExecuteCoroutine(Move(pos, stayTime), "ShowTarget");
        private IEnumerator Move(Vector2 pos, float stayTime)
        {
            float _elapsed = 0f;
            float _moveSpeed = 1f;

            CameraZoomInOut(stayTime);

            while (_elapsed < 1f)
            {
                transform.localPosition = Vector3.Lerp(orginPos, new Vector3(pos.x, pos.y, -10f),
                    _elapsed += Time.deltaTime * _moveSpeed / 1f);
                yield return null;
            }

            _elapsed = 0f;
            yield return UtilFunctions.WaitForSeconds(stayTime);
            while (_elapsed < 1f)
            {
                transform.localPosition = Vector3.Lerp(new Vector3(pos.x, pos.y, -10f), orginPos,
                    _elapsed += Time.deltaTime * _moveSpeed / 1f);
                yield return null;
            }
            transform.localPosition = orginPos;
            yield break;
        }

        public void CameraZoomInOut(float stayTime) => cc.ExecuteCoroutine(ZoomInOut(stayTime), "ZoomInOut");
        private IEnumerator ZoomInOut(float stayTime)
        {
            float _elapsed = 0f;
            float _zoomSpeed = 1f;

            while (_elapsed < 1f)
            {
                mCamera.orthographicSize = Mathf.Lerp(zoomOutSize, zoomInSize, _elapsed += Time.deltaTime * _zoomSpeed / 1f);
                yield return null;
            }

            yield return UtilFunctions.WaitForSeconds(stayTime);
            _elapsed = 0f;

            while (_elapsed < 1f)
            {
                mCamera.orthographicSize = Mathf.Lerp(zoomInSize, zoomOutSize, _elapsed += Time.deltaTime * _zoomSpeed / 1f);
                yield return null;
            }

            mCamera.orthographicSize = zoomOutSize;
            yield break;
        }
        
        public void ShakeCamera(float durateTime) => cc.ExecuteCoroutine(ShakeObject(durateTime), "Shake");
        private IEnumerator ShakeObject(float durateTime)
        {
            float _elapsed = 0f;

            while (_elapsed < durateTime)
            {
                var _posX = Random.Range(-0.2f, 0.2f);
                var _posY = Random.Range(-0.2f, 0.2f);

                transform.localPosition = new Vector3(_posX, _posY, -10f);
                _elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = orginPos;
            yield break;
        }

        private void Awake()
        {
            mCamera = GetComponent<Camera>();
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
