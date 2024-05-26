using UnityEngine;
using LMS.Utility;
using LMS.Enemy;
using LMS.User;
using LMS.UI;
using System.Runtime.CompilerServices;
using System.Collections;

namespace LMS.Manager
{
    public class PlayManager : MonoSingleton<PlayManager>
    {
        #region 카메라 기능
        [SerializeField] private CameraController mCamera;
        public void ShakeCamera(float durateTime) => mCamera.ShakeCamera(durateTime);
        #endregion

        #region 인 게임 필요 변수 및 메소드, 맵 관련 메소드
        [SerializeField] private GameObject rewardsButton;
        [SerializeField] private GaugeBar expBar;

        private MonsterSpawner monsterSpawner;

        [SerializeField] private Player player;
        private WeaponController wController;
        public Transform pTrans { get { return player.transform; } }
        public Vector2 GetPlayerPos { get { return player.transform.position; } }
        [SerializeField] private int playerLevel;
        [SerializeField] private float maxExp;
        [SerializeField] private float exp;
        private float Exp
        {
            get { return exp; }
            set
            {
                if (value >= maxExp)
                {
                    exp = value - maxExp;
                    LevelUp();
                }
                else exp = value;
            }
        }
        private float elapsedTime;
        public float ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }
        private void MapSet()
        {
            Vector2 _pos = transform.position;
            float _endX = PlayInfo.mapEndX;
            float _endY = PlayInfo.mapEndY;

            if (GetPlayerPos.x < -_endX) _pos += new Vector2((int)_endX, 0f);
            if (GetPlayerPos.x >= _endX) _pos += new Vector2(-(int)_endX, 0f);
            if (GetPlayerPos.y < -_endY) _pos += new Vector2(0f, (int)_endY);
            if (GetPlayerPos.y >= _endY) _pos += new Vector2(0f, -(int)_endY);

            transform.position = _pos;
        }
        #endregion

        #region 외부 상호작용 메소드
        public void PauseGame() => Time.timeScale = 0f;
        public void SlowPauseGame() => StartCoroutine(SlowPauseGameCoroutine());
        private IEnumerator SlowPauseGameCoroutine()
        {
            float _elapsed = 2f;
            while (_elapsed > 0.01f)
            {
                Time.timeScale = Mathf.Clamp((_elapsed -= Time.unscaledDeltaTime) / 2f, 0f, 1f);
                yield return null;
            }
            PauseGame();
            yield break;
        }
        public void PlayGame() => Time.timeScale = 1f;
        public void WeaponLevelUp(string wName) => wController.AddWeapon(wName);
        public int GetWeaponLevel(string wName) => wController.GetWeaponLevel(wName);
        private void LevelUp()
        {
            ++playerLevel;
            expBar.SetMaxGaugeValue(maxExp += maxExp * 0.5f);
            rewardsButton.SetActive(true);
            // 두 가지 선택지 제공
            // 레벨업을 통한 무기 추가 or 무기 업그레이드
        }
        public void UpdateExp(float value)
        {
            Exp += value;
            expBar.UpdateGaugeBar(Exp);
        }
        #endregion

        #region Init
        private void InitGameSetting()
        {
            monsterSpawner = new MonsterSpawner(player.transform);
            wController = new WeaponController(player.transform);

            playerLevel = 1;
            ElapsedTime = 0;
            player.Initialized();
            rewardsButton.SetActive(true);

            exp = 0f;
            maxExp = 100f;
            expBar.Initialized(maxExp);

            SetIgnoreCollision();
            // 또 뭐가 필요할까?
        }
        private void SetIgnoreCollision()
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Item"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Item"), LayerMask.NameToLayer("Item"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Item"), LayerMask.NameToLayer("WeaponObject"));
        }
        protected override void Awake()
        {
        }
        private void Start()
        {
            InitGameSetting();
        }
        #endregion


        [SerializeField] private UnityEngine.UI.Text text;
        void Update()
        {
            text.text = $"몬스터 갯수 : {CommonMonster.aliveMonsterCount}\nTIme Scale : {Time.timeScale}";
            if (Input.GetKeyDown(KeyCode.G)) player.Revive();
            if (Input.GetKeyDown(KeyCode.Space)) /*Time.timeScale = 1f;*/ Exp += maxExp;
            //if (Input.GetKeyDown(KeyCode.J)) Time.timeScale += 0.1f;
            //if (Input.GetKeyDown(KeyCode.H)) Time.timeScale -= 0.1f;
            MapSet();
        }
    }

}
