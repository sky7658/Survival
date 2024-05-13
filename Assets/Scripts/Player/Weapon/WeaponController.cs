using System.Collections;
using System.Collections.Generic;
using LMS.General;
using UnityEngine;

namespace LMS.User
{
    public class WeaponController
    {
        private List<Weapon> weapons = new List<Weapon>();
        private Dictionary<string, Weapon> weaponCaches = new Dictionary<string, Weapon>();

        public WeaponController(Transform pTrans) 
        {
            weaponCaches.Add(Base.WeaponInfo.wnameSO.Bow, new Bow(pTrans));
            weaponCaches.Add(Base.WeaponInfo.wnameSO.WizardBook, new WizardBook(pTrans));
            weaponCaches.Add(Base.WeaponInfo.wnameSO.Ring, new Ring(pTrans));
        }

        public void AddWeapon(string wName)
        {
            if(weaponCaches.TryGetValue(wName, out var weapon))
            {
                weapons.Add(weapon);
                weapon.WeaponActive = true;
                return;
            }
            Debug.Log("존재하지 않은 Weapon 이름입니다.");
        }

        public void RemoveWeapon(string wName)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].GetwName.Equals(wName))
                {
                    weapons[i].WeaponActive = false;
                    weapons.RemoveAt(i);
                    return;
                }
            }
            Debug.Log("존재하지 않은 Weapon 이름입니다.");
        }

        private Weapon GetWeapon(string wName)
        {
            foreach (var weapon in weapons) 
            {
                if (weapon.GetwName.Equals(wName)) return weapon;
            }
            Debug.Log("존재하지 않은 Weapon 이름입니다.");
            return null;
        }

        public void WeaponLevelUp(string wName) => GetWeapon(wName).LevelUp();
        public void AllWeaponLevelUp() => weapons.ForEach(weapon => weapon.LevelUp());
    }
}