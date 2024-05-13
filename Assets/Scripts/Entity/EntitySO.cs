using UnityEngine;
using LMS.Manager;

namespace LMS.General
{
    [CreateAssetMenu(fileName = "Entity SO", menuName = "Scriptable Object/Entity SO", order = int.MaxValue)]
    public class EntitySO : ScriptableObject
    {
        // 오브젝트 이름
        [SerializeField] private string objectName;
        public string ObjectName { get { return objectName; } }

        // 오브젝트 최대 체력
        [SerializeField] private float maxHp;
        public float MaxHp { get { return maxHp; } }

        // 오브젝트 기본 속력
        [SerializeField] private float basicSpeed;
        public float BasicSpeed { get { return basicSpeed; } }

        // 오브젝트 최대 속력
        [SerializeField] private float maxSpeed;
        public float MaxSpeed { get { return maxSpeed; } }

        // 오브젝트 방어력
        [SerializeField] private float basicDefense;
        public float BasicDefense { get { return basicDefense; } }
    }

    public static class EntitySOData
    {
        public static EntitySO playerSO = ResourceManager.Instance.GetSO<EntitySO>("PlayerSO");
        public static EntitySO batSO = ResourceManager.Instance.GetSO<EntitySO>("BatSO");
        public static EntitySO crabSO = ResourceManager.Instance.GetSO<EntitySO>("Crab");
        public static EntitySO pebbleSO = ResourceManager.Instance.GetSO<EntitySO>("Pebble");
        public static EntitySO slimeSO = ResourceManager.Instance.GetSO<EntitySO>("SlimeSO");
    }
}

