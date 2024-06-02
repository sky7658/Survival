using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LMS.Utility;
using LMS.Enemy;
using LMS.User;
using LMS.UI;
using LMS.ItemObject;

namespace LMS.Manager
{
    public enum PlayState { PLAY, PAUSE };
    public class PlayManager : MonoSingleton<PlayManager>
    {
        #region 카메라 기능
        [SerializeField] private CameraController mCamera;
        public void ShakeCamera(float durateTime) => mCamera.ShakeCamera(durateTime);
        public void ZoomInOutCamera(float durateTime, System.Action action = null) => mCamera.CameraZoomInOut(durateTime, action);
        public void ShowTargetCamera(Vector2 pos, float durateTime, System.Action action = null) => mCamera.ShowTarget(pos, durateTime, action);
        #endregion

        #region 인 게임 필요 변수 및 메소드, 맵 관련 메소드
        [Header("# Play UI")]
        [SerializeField] private GameObject rewardsButton;
        [SerializeField] private GaugeBar expBar;
        [SerializeField] private UnityEngine.UI.Text playerLevelText;
        [SerializeField] private MoneyUI moneyUI;
        [SerializeField] private int myMoney;

        private MonsterSpawner monsterSpawner;
        [Header("# Player")]
        [SerializeField] private Player player;
        private WeaponController wController;
        public Transform pTrans { get { return player.transform; } }
        public Vector2 GetPlayerPos { get { return player.transform.position; } }

        [Header("# Player Level")]
        [SerializeField] private int playerLevel;
        [SerializeField] private float maxExp;
        [SerializeField] private float exp;

        [Header("# PlayState")]
        [SerializeField] private PlayState pState;
        public PlayState PState
        {
            get{ return pState; }
            set { pState = value; }
        }
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
        //[HideInInspector]
        public bool BossStage
        {
            get
            {
                if (ElapsedTime >= PlayInfo.BossSpawnTime) return true;
                return false;
            }
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
        // 게임 설정 메소드-----------------------------------------------------------------------------------------------------
        public void PauseGame()
        {
            PState = PlayState.PAUSE;
            Time.timeScale = 0f;
        }
        public void SlowPauseGame(System.Action action = null) => StartCoroutine(SlowPauseGameCoroutine(action));
        private IEnumerator SlowPauseGameCoroutine(System.Action action)
        {
            float _elapsed = 2f;
            while (_elapsed > 0.01f)
            {
                Time.timeScale = Mathf.Clamp((_elapsed -= Time.unscaledDeltaTime) / 2f, 0f, 1f);
                yield return null;
            }
            PauseGame();
            if (action != null) action();
            yield break;
        }
        public void PlayGame()
        {
            PState = PlayState.PLAY;
            Time.timeScale = 1f;
        }
        public bool IsGamePlay => PState == PlayState.PLAY;
        //----------------------------------------------------------------------------------------------------------------------
        // 플레이어 레벨--------------------------------------------------------------------------------------------------------
        public void WeaponLevelUp(string wName) => wController.AddWeapon(wName);
        public int GetWeaponLevel(string wName) => wController.GetWeaponLevel(wName);
        private void LevelUp()
        {
            //playerLevelText.text = $"Lv : {++playerLevel}"; // Player Level Text 업데이트
            ++playerLevel; // 위에 코드로 수정할 예정

            expBar.SetMaxGaugeValue(maxExp += Mathf.FloorToInt(maxExp * 0.25f)); // Max Exp 연산
            rewardsButton.SetActive(true); // Rewards 선택
        }
        public void UpdateExp(float value)
        {
            Exp += value;
            expBar.UpdateGaugeBar(Exp);
        }
        //----------------------------------------------------------------------------------------------------------------------
        // 플레이어 돈----------------------------------------------------------------------------------------------------------
        public void UpdateMoney(int amount)
        {
            myMoney += amount;
            moneyUI.UpdateMoney(myMoney);
        }
        //----------------------------------------------------------------------------------------------------------------------
        #endregion

        #region Item 상호작용
        private List<ExpBall> expBalls = new List<ExpBall>();
        public void AddExpBall(ExpBall expBall) => expBalls.Add(expBall);
        public void DeleteExpBall(ExpBall expBall) => expBalls.Remove(expBall);
        public void GetExpBalls() => expBalls.ForEach(expBall => expBall.AutoGet());
        #endregion

        #region Init
        private void InitGameSetting()
        {
            monsterSpawner = new MonsterSpawner(player.transform);
            wController = new WeaponController(player.transform);

            ElapsedTime = 0;
            player.Initialized();
            rewardsButton.SetActive(true);

            playerLevel = 1;
            exp = 0f;
            maxExp = 100f;
            expBar.Initialized(maxExp);

            myMoney = 0;
            moneyUI.UpdateMoney(myMoney);

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


        void Update()
        {
            //TEST-------------------------------------------------------------------------------------------------
            playerLevelText.text = $"Lv : {playerLevel}\n몬스터 갯수 : {CommonMonster.aliveMonsterCount}\nTIme Scale : {Time.timeScale}";
            if (Input.GetKeyDown(KeyCode.G)) /*GetExpBalls();*/Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.F)) player.Revive();
            if (Input.GetKeyDown(KeyCode.Space)) /*Time.timeScale = 1f;*/ Exp += maxExp;
            //if (Input.GetKeyDown(KeyCode.J)) Time.timeScale += 0.1f;
            //if (Input.GetKeyDown(KeyCode.H)) Time.timeScale -= 0.1f;
            //TEST-------------------------------------------------------------------------------------------------

            MapSet();
        }
    }

}
