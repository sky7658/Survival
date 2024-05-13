using System.Collections;
using UnityEngine;
using LMS.User;
using LMS.Utility;
using Unity.Collections;

namespace LMS.General
{
    public struct WeaponInfo
    {
        public string wName;
        public float atk;
        public int penetrationCnt;
        public float speed;
    }
    public struct WeaponInfos
    {
        public WeaponInfo wInfo;
        public float coolTime;
        public int objectCount;

        public static WeaponInfos operator +(WeaponInfos a, WeaponInfos b)
        {
            a.wInfo.atk += b.wInfo.atk;
            a.wInfo.penetrationCnt += b.wInfo.penetrationCnt;
            a.wInfo.speed += b.wInfo.speed;
            a.coolTime -= b.coolTime; // ����
            a.objectCount += b.objectCount;

            return a;
        }
    }
    
    public abstract class Weapon
    {
        private bool wActive = false;
        public bool WeaponActive
        {
            get { return wActive; }
            set
            {
                if (wActive != value)
                {
                    wActive = value;
                    if (wActive) cc.ExecuteCoroutine(AutoAttack(), "AutoAttack");
                    else cc.OffAllCoroutines();
                }
            }
        }

        private CoroutineController cc;
        private readonly Transform pTrans;
        private WeaponInfos wInfos;
        public string GetwName => wInfos.wInfo.wName;

        public float Atk
        {
            get { return wInfos.wInfo.atk; }
            set { wInfos.wInfo.atk = value; }
        }
        protected WeaponInfo WInfo => wInfos.wInfo;
        private int weaponLevel;
        protected int ObjectCount => wInfos.objectCount;
        protected float keepTime;

        private Sprite spr;
        private void SetSprite(Sprite sprite)
        {
            if(sprite == null)
            {
                Debug.Log("Sprite Null Exception");
                return;
            }
            spr = sprite;
        }
        public Sprite GetSprite() => spr;
        public Weapon(Transform pTrans, WeaponSO wso)
        {
            if (wso == null) return;
            cc = new CoroutineController();

            this.pTrans = pTrans;
            InitInfo(wso);
            InitCoroutine();
        }
        private void InitInfo(WeaponSO wso)
        {
            wInfos.wInfo.wName = wso.ObjName;
            wInfos.wInfo.atk = wso.Atk;
            wInfos.wInfo.penetrationCnt = wso.PenetrationCnt;
            wInfos.wInfo.speed = wso.Speed;
            wInfos.coolTime = wso.CoolTime;
            wInfos.objectCount = wso.ObjectCount;
            if (!Base.WeaponInfo.wonames.TryGetValue(GetwName, out var woname))
            {
                Debug.Log($"{GetwName} is not exist in WeaponNames");
                woname = "NULL";
            }
            if (!Base.WeaponInfo.woKeepTimes.TryGetValue(woname, out keepTime))
            {
                Debug.Log($"{GetwName} is not exist in WeaponObjectNames");
                keepTime = 0f;
            }
            weaponLevel = 1;
        }
        private void InitCoroutine()
        {
            cc.AddCoroutine("AutoAttack");
            cc.AddCoroutine("Attack");
        }
        public abstract IEnumerator Attack(Transform pTrans);
        public virtual void LevelUp()
        {
            if (weaponLevel == Base.WeaponInfo.MaxLevel || weaponLevel < 1) return;
            if (!Base.WeaponInfo.wlevelInfo.TryGetValue(GetwName, out var _info))
            {
                Debug.Log($"{GetwName} is not exist in WeaponNames");
                return;
            }
            wInfos += _info[++weaponLevel];
        }
        private IEnumerator AutoAttack()
        {
            while (true)
            {
                if (!WeaponActive) yield break;
                cc.ExecuteCoroutine(Attack(pTrans), "Attack");
                yield return UtilFunctions.WaitForSeconds(wInfos.coolTime + keepTime);
            }
        }
    }
}
