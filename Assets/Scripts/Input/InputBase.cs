using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Controller
{
    public interface InputBase
    {
        // ���콺
        // Ŭ��
        bool isClick { get; }
        // Ŭ�� ��ǥ
        Vector2 clickPos { get; }
        //��
        float wheelMount { get; }
        // �̵�
        float x { get; }
        float y { get; }
    }
}
