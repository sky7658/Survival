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
            base.InitCoroutine(); // �� �ٸ� ���� �߰��Ұ� ������ �����Ұ̴ϴ�.
        }
        private void Start()
        {
            Initialized("Bow"); // �����Ұ̴ϴ�
        }
        private void Awake() // �굵�� ����
        {
            InitComponent();
        }
        public void UpdateState() => stateM.UpdateState();
        private void Update() // ��͵�?~~~ ����
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                wController.AllWeaponLevelUp();
            }
            stateM.UpdateState();
        }
    }
}