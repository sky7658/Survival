using LMS.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.User
{
    public class MagneticSkill : Skill
    {
        protected override void Execute()
        {
            PlayManager.Instance.GetExpBalls();
        }
        private void Start()
        {
            Initialized(10f);
        }
    }
}
