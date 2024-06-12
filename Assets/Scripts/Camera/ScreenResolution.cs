using UnityEngine;
using System;

public class ScreenResolution : MonoBehaviour
{
    private void SetScreenSize()
    {
        Camera.main.rect = new Rect(0, 0, 1, 1);
        GL.Clear(true, true, Color.black);

        float targetWidthAspect = 16.0f;

        //���� ȭ�� ����
        float targetHeightAspect = 9.0f;

        //���� ī�޶�
        Camera mainCamera = Camera.main;

        mainCamera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthtadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        // 16_10�������� ���ΰ� �F�ٸ�(4_3 ����)
        // 16_10�������� ���ΰ� ª�ٸ�(16_9 ����)
        // ���� ������ 0���� ������ش�
        if (heightRatio > widthRatio)
            widthtadd = 0.0f;
        else
            heightadd = 0.0f;
        mainCamera.rect = new Rect(
            mainCamera.rect.x + Math.Abs(widthtadd),
            mainCamera.rect.y + Math.Abs(heightadd),
            mainCamera.rect.width + (widthtadd * 2),
            mainCamera.rect.height + (heightadd * 2));
    }

    private void Awake()
    {
        SetScreenSize();
    }
    private void OnPreCull()
    {
        Rect _cpyRect = Camera.main.rect;
        Camera.main.rect = new Rect(0, 0, 1, 1);
        GL.Clear(true, true, Color.black);
        Camera.main.rect = _cpyRect;
    }
}
