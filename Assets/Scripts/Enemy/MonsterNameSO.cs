using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    [CreateAssetMenu(fileName = "MonsterName SO", menuName = "Scriptable Object/MonsterName SO")]
    public class MonsterNameSO : ScriptableObject
    {
        [SerializeField] private string bat;
        public string Bat { get { return bat; } }

        [SerializeField] private string crab;
        public string Crab { get { return crab; } }

        [SerializeField] private string pebble;
        public string Pebble { get {  return pebble; } }

        [SerializeField] private string slime;
        public string Slime { get {  return slime; } }
    }

}
