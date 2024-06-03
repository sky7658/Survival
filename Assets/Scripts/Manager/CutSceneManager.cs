using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMS.Utility;

namespace LMS.Manager
{
    public class CutSceneManager : MonoSingleton<CutSceneManager>
    {
        public bool isCutSceneMode;
        public void StartBossModeCutScene(Vector2 boss)
        {
            PlayManager.Instance.PState = PlayState.PAUSE;
            isCutSceneMode = true;
            PlayManager.Instance.ZoomInOutCamera(1f);
            PlayManager.Instance.ShowTargetCamera(boss, 1f, () => { isCutSceneMode = false; PlayManager.Instance.PState = PlayState.PLAY; });
        }
    }
}