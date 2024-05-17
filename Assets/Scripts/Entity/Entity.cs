using UnityEngine;
using LMS.Manager;
using LMS.Utility;
using System.Collections;

namespace LMS.General
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntitySO entitySO;
        protected void InitSO()
        {
            entitySO = entitySO == null ? ResourceManager.Instance.GetSO<EntitySO>(ObjectName) : entitySO;

            hp = entitySO.MaxHp;
            speed = entitySO.BasicSpeed;
            defense = entitySO.BasicDefense;
        }

        public string ObjectName { get { return entitySO.ObjectName; } }
        public float MaxHp { get { return entitySO.MaxHp; } }
        [SerializeField] private float hp;
        public float Hp
        {
            get { return hp; }
            protected set { hp = Mathf.Clamp(value, 0, MaxHp); }
        }
        [SerializeField] private float speed;
        public virtual float Speed
        {
            get { return speed; }
            protected set { speed = Mathf.Clamp(value, entitySO.BasicSpeed, entitySO.MaxSpeed); }
        }
        [SerializeField] private float defense; // % ������
        public float Defense
        {
            get { return defense; }
            protected set { defense = value; }
        }

        private Rigidbody2D rb;
        public void Move(Vector2 vec)
        {
            rb.velocity = vec * Speed;
        }
        public void AddForce(Vector2 vec, ForceMode2D mode)
        {
            rb.AddForce(vec * Speed, mode);
        }
        private SpriteRenderer spr;
        protected SpriteRenderer GetSpr { get { return spr; } }
        public void FlipX(bool set) => spr.flipX = set;
        public bool GetFlipX => spr.flipX;
        public void SetOriginColor() => SetColor(EntityInfo.originColor);
        public void SetColor(Color32 color) => spr.color = color;

        private Animator anim;
        public virtual void SetAnimation(string animName, bool set) => anim.SetBool(animName, set);
        public virtual void SetAnimation(string animName) => anim.SetTrigger(animName);

        private Collider2D col;
        protected void SetCollider(bool set) => col.enabled = set;
        protected Vector2 GetColliderCenter()
        {
            if (col.enabled) return col.offset;
            Debug.Log("Collider2D is Disabled");
            return Vector2.zero;
        }
        protected virtual void InitComponent()
        {
            if (TryGetComponent<Rigidbody2D>(out var rigidbody)) rb = rigidbody;
            if (TryGetComponent<SpriteRenderer>(out var renderer)) spr = renderer;
            if (TryGetComponent<Animator>(out var animator)) anim = animator;
            if (TryGetComponent<Collider2D>(out var collider)) col = collider;
        }
        public virtual void Initialized()
        {
            InitSO();
            InitCoroutine();
        }

        public virtual void TakeDamage(float value, Vector2 vec = default)
        {
            if (value > 0)
                cc.ExecuteCoroutine(SRUtilFunction.KeepSpriteColorTime(spr, EntityInfo.originColor, EntityInfo.hitColor, EntityInfo.keepHitTime), "HitColor");
            Hp -= (value - value / 100 * defense);
        }
        
        public void Recovery(float value)
        {
            Hp += value;
        }

        protected CoroutineController cc = new CoroutineController();
        protected virtual void InitCoroutine()
        {
            cc.AddCoroutine("HitColor");
            cc.AddCoroutine("Dead");
        }
        public virtual void Dead()
        {
            SetCollider(false);
        }
    }
}
