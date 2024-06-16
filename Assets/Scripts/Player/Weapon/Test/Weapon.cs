using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Test
{
    public interface IWeaponUpgrade
    {
        public int GetWeaponLevel { get; }
        public void LevelUp();
    }
    public class WeaponInfo
    {
        public string wname;
        public float atk;
        public int penetrationCnt;
        public float speed;
        public float coolTime;
        public int objectCount;
        public int level;
        
        public WeaponInfo(string wname, float atk, int penetrationCnt, float speed, float coolTime, int objectCount, int level)
        {
            this.wname = wname;
            this.atk = atk;
            this.penetrationCnt = penetrationCnt;
            this.speed = speed;
            this.coolTime = coolTime;
            this.objectCount = objectCount;
            this.level = level;
        }

        public static WeaponInfo operator +(WeaponInfo first, WeaponInfo second)
        {
            first.atk += second.atk;
            first.penetrationCnt += second.penetrationCnt;
            first.speed += second.speed;
            first.coolTime -= second.coolTime;
            first.objectCount += second.objectCount;

            return first;
        }
    }
    public class Weapon : IWeaponUpgrade
    {
        private WeaponInfo weaponInfo;

        public Weapon()
        {

        }

        #region Init
        private void InitWeaponInfo(User.WeaponSO wso)
        {
            weaponInfo = new WeaponInfo(wso.ObjName, wso.Atk, wso.PenetrationCnt, wso.Speed, wso.CoolTime, wso.ObjectCount, 1);

        }
        #endregion

        #region WeaponLevel
        public int GetWeaponLevel { get { return weaponInfo.level; } }
        public void LevelUp()
        {
            // Level Up ·ÎÁ÷
            weaponInfo.level++;
        }
        #endregion
    }
}
