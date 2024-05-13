using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Controller
{
    public interface InputBase
    {
        // 마우스
        // 클릭
        bool isClick { get; }
        // 클릭 좌표
        Vector2 clickPos { get; }
        //휠
        float wheelMount { get; }
        // 이동
        float x { get; }
        float y { get; }
    }
}
