using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.General
{
    public interface IDamageable
    {
        public void TakeDamage(float value, Vector2 vec = default);
        public void Recovery(float value);
    }

}
