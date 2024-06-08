using System.Collections.Generic;

namespace LMS.User
{
    public abstract class Ability
    {
        public static readonly int abilityMaxLevel = 4;

        protected List<int> priceByLevel = new List<int>();
        protected List<float> ratioByLevel = new List<float>();

        private int level;
        public int GetLevel { get { return level; } }
        protected float basicValue;
        private float abilityValue;
        public float AbilityValue
        {
            get
            {
                return abilityValue;
            }
            protected set { abilityValue = value; }
        }
        public Ability(float basicValue, int level)
        {
            this.level = level;
            this.basicValue = basicValue;
        }
        public void UpgradeAbility()
        {
            abilityValue = GetValueByLevel(++level);
        }

        public int GetPriceByNextLevel() => GetPriceByLevel(level + 1);
        private int GetPriceByLevel(int level)
        {
            if (priceByLevel.Count - 1 < level || level < 0)
                return default(int);
            return priceByLevel[level];
        }
        public float GetValueByCurrentLevel() => GetValueByLevel(level);
        protected virtual float GetValueByLevel(int level)
        {
            if (priceByLevel.Count - 1 < level || level < 0)
                return basicValue;
            return basicValue + basicValue * GetRatioByLevel(level);
        }

        public float GetRatioByNextLevel() => GetRatioByLevel(level + 1);
        private float GetRatioByLevel(int level)
        {
            if (priceByLevel.Count - 1 < level || level < 0)
                return default(float);
            return ratioByLevel[level];
        }
    }
    public class Shoes : Ability
    {
        public static readonly string abilityName = "Shoes";
        public Shoes(float basicValue, int level = -1) : base(basicValue, level)
        {
            priceByLevel.Add(500);
            priceByLevel.Add(3000);
            priceByLevel.Add(13000);
            priceByLevel.Add(30000);

            ratioByLevel.Add(0.2f);
            ratioByLevel.Add(0.4f);
            ratioByLevel.Add(0.6f);
            ratioByLevel.Add(0.8f);

            AbilityValue = GetValueByCurrentLevel();
        }
    }
    public class Hat : Ability
    {
        public static readonly string abilityName = "Hat";
        public Hat(float basicValue, int level = -1) : base(basicValue, level)
        {
            priceByLevel.Add(500);
            priceByLevel.Add(3000);
            priceByLevel.Add(13000);
            priceByLevel.Add(30000);

            ratioByLevel.Add(0.2f);
            ratioByLevel.Add(0.4f);
            ratioByLevel.Add(0.6f);
            ratioByLevel.Add(0.8f);

            AbilityValue = GetValueByCurrentLevel();
        }
        protected override float GetValueByLevel(int level)
        {
            if (priceByLevel.Count - 1 < level || level < 0)
                return basicValue;
            return basicValue + ratioByLevel[level];
        }
    }
    public class Candy : Ability
    {
        public static readonly string abilityName = "Candy";
        public Candy(float basicValue, int level = -1) : base(basicValue, level)
        {
            priceByLevel.Add(500);
            priceByLevel.Add(3000);
            priceByLevel.Add(13000);
            priceByLevel.Add(30000);

            ratioByLevel.Add(0.2f);
            ratioByLevel.Add(0.4f);
            ratioByLevel.Add(0.6f);
            ratioByLevel.Add(0.8f);

            AbilityValue = GetValueByCurrentLevel();
        }
    }
}
