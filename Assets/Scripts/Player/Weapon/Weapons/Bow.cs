using System.Collections;
using UnityEngine;
using LMS.General;
using LMS.Controller;
using Unity.Properties;

namespace LMS.User
{
    public class Bow : Weapon
    {
        private float baseKeepTime;
        public Bow(Transform pTrans) : base(pTrans, Base.WeaponInfo.bowSO) 
        {
            baseKeepTime = keepTime;
            keepTime *= ObjectCount;
        }
        public override void LevelUp()
        {
            base.LevelUp();
            keepTime = baseKeepTime;
        }
        public override IEnumerator Attack(Transform pTrans)
        {
            int _index = 0;
            Vector2 _addPos;

            if (InputManager.isMoveVectorY && !InputManager.isMoveVectorX) _index = 1;
            else if (InputManager.isMoveVectorY && InputManager.isMoveVectorX) _index = 2;

            Vector2 range = Base.WeaponInfo.arrowPosRange[_index];

            for (int i = 0; i < ObjectCount; i++)
            {
                if (!WeaponActive) yield break;
                if(!Base.WeaponInfo.wonames.TryGetValue(GetwName, out var woname))
                {
                    Debug.Log($"{GetwName} is not exist in WeaponNames");
                    yield break;
                }

                _addPos = new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y));

                var arrow = Utility.ObjectPool.Instance.GetObject<Arrow>(woname);
                arrow.Initialized(WInfo, (Vector2)pTrans.position + _addPos);
                yield return Utility.UtilFunctions.WaitForSeconds(baseKeepTime);
            }
            yield break;
        }
    }
}
