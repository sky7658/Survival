using UnityEngine;
using LMS.General;
using LMS.State;
using LMS.Utility;
using LMS.UI;
using System.Collections;
using LMS.Manager;

namespace LMS.User
{
    public class Player : Entity
    {
        private PlayerStateMachine stateM;
        [SerializeField] private GaugeBar hpBar;
        private bool invincible = false;
        public override void Dead()
        {
            base.Dead();
            cc.OffCoroutine("HitColor");
            SetOriginColor();
            PlayManager.Instance.SlowPauseGame();
            SetAnimatorMode(AnimatorUpdateMode.UnscaledTime);
        }
        public void Revive()
        {
            PlayManager.Instance.PlayGame();
            Recovery(MaxHp);
            SetCollider(true);
            SetAnimatorMode(AnimatorUpdateMode.Normal);
            cc.ExecuteCoroutine(Invincible(), "Invincible");
        }
        private IEnumerator Invincible()
        {
            invincible = true;
            yield return UtilFunctions.WaitForSeconds(1f);
            invincible = false;
            yield break;
        }
        public override void TakeDamage(float value, Vector2 vec = default)
        {
            if (invincible) return;
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
            base.InitCoroutine();
            cc.AddCoroutine("Invincible");
        }
        private void Awake()
        {
            InitComponent();
        }
        private void FixedUpdate()
        {
            stateM.UpdateState();
        }
        private void Update()
        {
            stateM.ChangeState();
        }
        private void LateUpdate()
        {
            if (invincible) SRUtilFunction.SetColor(GetSpr, 0.5f);
        }
    }
}