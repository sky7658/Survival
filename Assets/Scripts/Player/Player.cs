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
        [SerializeField] private GaugeBar hpBar;
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
        public override void Initialized()
        {
            base.Initialized();
            hpBar.Initialized(MaxHp);
            stateM = new PlayerStateMachine(this);
        }
        protected override void InitCoroutine()
        {
            base.InitCoroutine(); // 별 다른 내용 추가할거 없으면 삭제할겁니다.
        }
        private void Awake()
        {
            InitComponent();
        }
        private void Update()
        {
            stateM.ChangeState();
        }
        private void FixedUpdate()
        {
            stateM.UpdateState();
        }
    }
}