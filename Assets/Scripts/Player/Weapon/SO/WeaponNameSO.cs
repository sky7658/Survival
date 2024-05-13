using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    [CreateAssetMenu(fileName = "WeaponName SO", menuName = "Scriptable Object/WeaponName SO", order = int.MaxValue)]
    public class WeaponNameSO : ScriptableObject
    {
        [SerializeField] private string bow;
        public string Bow { get { return bow; } }
        [SerializeField] private string wizardBook;
        public string WizardBook { get { return wizardBook; } }
        [SerializeField] private string ring;
        public string Ring { get { return ring; } }
        [SerializeField] private string arrow;
        public string Arrow { get { return arrow; } }
        [SerializeField] private string sword;
        public string Sword { get { return sword; } }
        [SerializeField] private string gemStone;
        public string GemStone { get { return gemStone; } }
    }
}