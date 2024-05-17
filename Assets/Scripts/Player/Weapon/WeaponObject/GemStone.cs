using UnityEngine;
using LMS.Utility;
using System.Collections;

namespace LMS.User
{
    public class GemStone : WeaponObject
    {
        private float createTime;
        private CoroutineController cc = new CoroutineController();

        public override void Initialized(General.WeaponInfo wInfo)
        {
            cc.AddCoroutine("Start");
            cc.AddCoroutine("End");
            cc.AddCoroutine("AfterExecute");

            base.Initialized(wInfo);
            createTime = keepTime / 5f;

            cc.OffAllCoroutines();

            transform.localScale = Vector2.zero;
            cc.ExecuteCoroutine(SRUtilFunction.SetScaleLerp(transform, Vector2.one, createTime), "Start");
            cc.ExecuteCoroutine(AfterExecute(), "AfterExecute");
        }

        public void Spin(Vector2 pos, float deg, float circleR)
        {
            var rad = Mathf.Deg2Rad * deg;
            var y = circleR * Mathf.Cos(rad);
            var x = circleR * Mathf.Sin(rad);
            transform.position = pos + new Vector2(x, y);
        }
        private void Release()
        {
            cc.ExecuteCoroutine(SRUtilFunction.SetScaleLerp(transform, Vector2.zero, createTime, () =>
            ReturnObject()), "End");
        }
        protected override void ReturnObject() => Utility.ObjectPool.Instance.ReturnObject(this, GetWoName);
        private IEnumerator AfterExecute()
        {
            yield return UtilFunctions.WaitForSeconds(keepTime - createTime);
            Release();
            yield break;
        }
    }
}