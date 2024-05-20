using UnityEngine;
using System.Collections.Generic;
using LMS.General;
using LMS.State;
using LMS.Utility;
using LMS.UI;

namespace LMS.User
{
    public class Player : Entity
    {
        private PlayerStateMachine stateM;
        private WeaponController wController;
        [SerializeField] private GaugeBar hpBar;

        public void LevelUp(string wName) => wController.AddWeapon(wName);
        public override void Dead()
        {
            base.Dead();
            cc.ExecuteCoroutine(SRUtilFunction.SetSpriteColorTime(GetSpr, new Color32(0, 0, 0, 0), EntityInfo.deadTime), "Dead");
        }
        public override void TakeDamage(float value, Vector2 vec = default)
        {
            base.TakeDamage(value, vec);
            hpBar.UpdateGaugeBar(Hp);
        }
        public override void Recovery(float value)
        {
            base.Recovery(value);
            hpBar.UpdateGaugeBar(Hp);
        }
        public void Initialized(string startWeapon)
        {
            Initialized();
            hpBar.Initialized(MaxHp);
            wController = new WeaponController(transform);
            stateM = new PlayerStateMachine(this);

            LevelUp(startWeapon);
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine(); // 별 다른 내용 추가할거 없으면 삭제할겁니다.
        }
        private void Start()
        {
            Initialized("Bow"); // 삭제할겁니다
        }
        private void Awake() // 얘도요 ㅎㅎ
        {
            InitComponent();
        }
        public void UpdateState() => stateM.UpdateState();
        private void Update() // 요것두?~~~ 삭제
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                wController.AllWeaponLevelUp();
            }
            stateM.UpdateState();
        }
    }
}