using LMS.Manager;
using LMS.User;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Base
{
    public static class WeaponInfo
    {
        public static readonly WeaponSO wizardBookSO = ResourceManager.Instance.GetSO<WeaponSO>("WizardBookSO");
        public static readonly WeaponSO bowSO = ResourceManager.Instance.GetSO<WeaponSO>("BowSO");
        public static readonly WeaponSO ringSO = ResourceManager.Instance.GetSO<WeaponSO>("RingSO");

        public static readonly WeaponNameSO wnameSO = ResourceManager.Instance.GetSO<WeaponNameSO>("WeaponNameSO");

        public static readonly Dictionary<string, string> WeaponDescriptions = new Dictionary<string, string>()
        {
            { wnameSO.Bow, "바라보는 방향을 향해 투사체를 발사합니다." },
            { wnameSO.WizardBook, "일정 범위 내에 있는 가까운 적을 공격합니다." },
            { wnameSO.Ring, "주변을 맴도는 오브젝트를 생성합니다." }
        };

        public static readonly string spellEffectName = "SpellEffect";

        public static readonly Dictionary<string, string> wonames = new Dictionary<string, string>()
        {
            { wnameSO.Bow, wnameSO.Arrow },
            { wnameSO.WizardBook, wnameSO.Sword },
            { wnameSO.Ring, wnameSO.GemStone }
        };

        public static readonly Dictionary<string, float> woKeepTimes = new Dictionary<string, float>()
        {
            { wnameSO.Arrow, 0.1f },
            { wnameSO.Sword, 0f },
            { wnameSO.GemStone, 5f }
        };

        public static readonly int MaxLevel = 9;

        public static readonly Dictionary<int, General.WeaponInfos> bowLevelInfo = new Dictionary<int, General.WeaponInfos>()
        {
            { 2, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 3, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 5f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 4, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 1, speed = 50f },
                                          coolTime = 0f, objectCount = 0 } },
            { 5, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.2f, objectCount = 0 } },
            { 6, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 7, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.2f, objectCount = 0 } },
            { 8, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 9, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Bow, atk = 5f, penetrationCnt = 1, speed = 0f },
                                          coolTime = 0.2f, objectCount = 1 } }
        };
        public static readonly Dictionary<int, General.WeaponInfos> wizardBookLevelInfo = new Dictionary<int, General.WeaponInfos>()
        {
            { 2, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 3, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 4, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.5f, objectCount = 0 } },
            { 5, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 6, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 10f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 7, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.5f, objectCount = 0 } },
            { 8, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.5f, objectCount = 1 } },
            { 9, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.WizardBook, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.5f, objectCount = 1 } }
        };
        public static readonly Dictionary<int, General.WeaponInfos> ringLevelInfo = new Dictionary<int, General.WeaponInfos>()
        {
            { 2, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 3, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.2f, objectCount = 0 } },
            { 4, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 50f },
                                          coolTime = 0f, objectCount = 1 } },
            { 5, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 5f, penetrationCnt = 0, speed = 50f },
                                          coolTime = 0.3f, objectCount = 0 } },
            { 6, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0f, objectCount = 1 } },
            { 7, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 50f },
                                          coolTime = 0.5f, objectCount = 0 } },
            { 8, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 50f },
                                          coolTime = 0f, objectCount = 1 } },
            { 9, new General.WeaponInfos { wInfo =  new General.WeaponInfo { wName = wnameSO.Ring, atk = 0f, penetrationCnt = 0, speed = 0f },
                                          coolTime = 0.5f, objectCount = 1 } }
        };

        public static readonly Dictionary<string, Dictionary<int, General.WeaponInfos>> wlevelInfo 
            = new Dictionary<string, Dictionary<int, General.WeaponInfos>>()
        {
            { wnameSO.Bow, bowLevelInfo },
            { wnameSO.WizardBook, wizardBookLevelInfo },
            { wnameSO.Ring, ringLevelInfo }
        };

        public static Vector2[] arrowPosRange = new Vector2[]
        {
            new Vector2(0, 0.2f),
            new Vector2(0.2f, 0),
            new Vector2(0.2f, 0.2f)
        };
    }
}