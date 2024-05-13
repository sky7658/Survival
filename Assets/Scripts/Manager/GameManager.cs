using LMS.User;
using LMS.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        protected override bool UseDontDestroy() => true;
    }


}
