using LMS.General;
using LMS.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public class WizardBook : Weapon
    {
        public WizardBook(Transform pTrans) : base(pTrans, Base.WeaponInfo.wizardBookSO) { }
        public override IEnumerator Attack(Transform pTrans)
        {
            var _spellEffect = ObjectPool.Instance.GetObject<SpellEffect>(Base.WeaponInfo.spellEffectName);
            _spellEffect.Initialized(WInfo, pTrans.position, ObjectCount);
            yield break;
        }
    }
}
