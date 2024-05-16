using LMS.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Manager
{
    public class PlayManager : MonoBehaviour
    {
        public Transform player;
        void Update()
        {
            if (player.transform.position.x < -4.12f)
            {
                transform.position = new Vector2(transform.position.x + 4.12f, 0f);
            }
        }
    }

}
