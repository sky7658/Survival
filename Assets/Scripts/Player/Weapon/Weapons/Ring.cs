using UnityEngine;
using LMS.General;
using System.Collections;
using System.Collections.Generic;
using LMS.Manager;

namespace LMS.User
{
    public class Ring : Weapon
    {
        private List<GemStone> stones = new List<GemStone>();
        public Ring(Transform pTrans) : base(pTrans, Base.WeaponInfo.ringSO) { }
        public override IEnumerator Attack(Transform pTrans)
        {
            float _elapsed = 0f;
            float _deg = 0f;

            for (int i = 0; i < ObjectCount; i++)
            {
                if(!Base.WeaponInfo.wonames.TryGetValue(GetwName, out var woname))
                {
                    Debug.Log($"{GetwName} is not exist in WeaponNames");
                    yield break;
                }

                if(i.Equals(stones.Count)) stones.Add(Utility.ObjectPool.Instance.GetObject<GemStone>(woname));
                else stones[i] = Utility.ObjectPool.Instance.GetObject<GemStone>(woname);
                stones[i].Initialized(WInfo);
            }

            while (_elapsed < keepTime)
            {
                if (!WeaponActive) yield break;

                _elapsed += Time.deltaTime;
                _deg += Time.deltaTime * WInfo.speed;
                _deg %= 360f;
                for (int i = 0; i < stones.Count; i++) stones[i].Spin(pTrans.position, _deg + i * 360 / stones.Count, 2f);
                yield return null;
            }
            yield break;
        }
    }
}
