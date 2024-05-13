using UnityEngine;
using System.Collections.Generic;
using LMS.General;
using LMS.State;
using LMS.Utility;

namespace LMS.User
{
    public class Player : Entity
    {
        private PlayerStateMachine stateM;
        private WeaponController wController;

        public override void Dead()
        {
            base.Dead();
            cc.ExecuteCoroutine(SRUtilFunction.SetSpriteColorTime(GetSpr, new Color32(0, 0, 0, 0), EntityInfo.deadTime), "Dead");
        }

        public override void Initialized()
        {
            base.Initialized();

            wController = new WeaponController(transform);
            stateM = new PlayerStateMachine(this);

            wController.AddWeapon(Base.WeaponInfo.wnameSO.Bow);
            //wController.AddWeapon(Base.WeaponInfo.wnameSO.Ring);
            //wController.AddWeapon(Base.WeaponInfo.wnameSO.WizardBook);
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine();
        }

        private void Awake()
        {
            InitComponent();
        }
        private void Start()
        {
            Initialized();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wController.AllWeaponLevelUp();
            }
            if (Input.GetKeyDown(KeyCode.F)) wController.RemoveWeapon("Bow");
            if (Input.GetKeyDown(KeyCode.G)) wController.AddWeapon("Bow");
            stateM.UpdateState();
        }
    }
}