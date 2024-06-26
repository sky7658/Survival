using UnityEngine;
using LMS.Manager;
using LMS.Utility;

namespace LMS.General
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntitySO entitySO;
        protected virtual void InitSO()
        {
            maxHp = entitySO.MaxHp;
            hp = entitySO.MaxHp;
            speed = entitySO.BasicSpeed;
            defense = entitySO.BasicDefense;
        }
        public string ObjectName { get { return entitySO.ObjectName; } }
        private float maxHp;
        public float MaxHp 
        { 
            get { return maxHp; }
            protected set { maxHp = value; }
        }
        [SerializeField] private float hp;
        public float Hp
        {
            get { return hp; }
            protected set { hp = Mathf.Clamp(value, 0, MaxHp); }
        }
        public float MaxSpeed { get { return entitySO.MaxSpeed; } }
        [SerializeField] private float speed;
        public virtual float Speed
        {
            get { return speed; }
            protected set { speed = Mathf.Clamp(value, entitySO.BasicSpeed, MaxSpeed); }
        }
        [SerializeField] private float defense; // % ������
        public float Defense
        {
            get { return defense; }
            protected set { defense = value; }
        }

        private Rigidbody2D rb;
        public void SetBodyType(RigidbodyType2D type) => rb.bodyType = type;
        public void Move(Vector2 vec)
        {
            rb.velocity = vec * Speed * Time.fixedDeltaTime;
        }
        private SpriteRenderer spr;
        protected SpriteRenderer GetSpr { get { return spr; } }
        public void FlipX(bool set) => spr.flipX = set;
        public bool GetFlipX => spr.flipX;
        public void SetOriginColor() => SetColor(EntityInfo.originColor);
        public void SetColor(Color32 color) => spr.color = color;

        private Animator anim;
        protected void SetAnimatorMode(AnimatorUpdateMode mode) => anim.updateMode = mode;
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
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
        public virtual void TakeDamage(float value, Vector2 vec = default)
        {
            if (value > 0 && Hp > value)
                cc.ExecuteCoroutine(SRUtilFunction.KeepSpriteColorTime(spr, EntityInfo.originColor, EntityInfo.hitColor, EntityInfo.keepHitTime), "HitColor");
            Hp -= (value - value / 100 * defense);
        }
        public virtual void Recovery(float value)
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
