using System;
using System.Collections;
using UnityEngine;

namespace LMS.Utility
{
    public static class SRUtilFunction
    {
        public static IEnumerator SetSpriteColorTime(SpriteRenderer sr, Color32 endColor, float t)
        {
            float _elapsed = 0f;
            Color32 startColor = sr.color;

            while (_elapsed < t)
            {
                sr.color = Color.Lerp(startColor, endColor, _elapsed / t);
                _elapsed += Time.deltaTime;
                yield return null;
            }
            sr.color = endColor;
            yield break;
        }

        public static IEnumerator KeepSpriteColorTime(SpriteRenderer sr, Color32 keepColor, float t)
        {
            Color32 originColor = sr.color;

            sr.color = keepColor;
            yield return UtilFunctions.WaitForSeconds(t);
            sr.color = originColor;

            yield break;
        }

        public static IEnumerator SetScaleLerp(Transform trans, Vector2 tScale, float t, Action del = null)
        {
            Vector2 originScale = trans.localScale;
            float _elapsed = 0f;

            while (_elapsed < t)
            {
                _elapsed += Time.deltaTime;
                trans.localScale = Vector2.Lerp(originScale, tScale, _elapsed / t);
                yield return null;
            }

            trans.localScale = tScale;
            if (del != null) del();

            yield break;
        }
    }
}