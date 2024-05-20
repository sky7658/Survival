using UnityEngine;
using LMS.Utility;
using LMS.Enemy;
using LMS.User;
using LMS.UI;
using LMS.ItemObject;

namespace LMS.Manager
{
    public class PlayManager : MonoSingleton<PlayManager>
    {
        [SerializeField] private GaugeBar expBar;
        [SerializeField] private Player player;
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

        public Vector2 GetPlayerPos { get { return player.transform.position; } }
        [SerializeField] private CameraController mCamera;

        private MonsterSpawner monsterSpawner;

        private float elapsedTime;
        private float ElapsedTime
        {
            get { return elapsedTime += Time.deltaTime; }
            set { elapsedTime = value; }
        }

        #region ī�޶� ���
        public void ShakeCamera(float durateTime) => mCamera.ShakeCamera(durateTime);
        #endregion

        #region ���� �÷��� ����
        private void InitGameSetting()
        {
            monsterSpawner = new MonsterSpawner(player.transform);

            playerLevel = 1;
            ElapsedTime = 0;
            player.Initialized(Base.WeaponInfo.wnameSO.Bow);

            exp = 0f;
            maxExp = 100f;
            expBar.Initialized(maxExp);
            // ���ͽ����� �۵�
            // �� ���� �ʿ��ұ�?
        }
        #endregion

        #region �÷��̾�� ��ȣ�ۿ�
        private void LevelUp()
        {
            ++playerLevel;
            expBar.SetMaxGaugeValue(maxExp += maxExp * 0.5f);
            // �� ���� ������ ����
            // �������� ���� ���� �߰� or ���� ���׷��̵�
        }
        public void UpdateExp(float value)
        {
            Exp += value;
            expBar.UpdateGaugeBar(Exp);
        }
        #endregion


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
        protected override void Awake()
        {
            
        }
        private void Start()
        {
            monsterSpawner = new MonsterSpawner(player.transform);

            exp = 0f;
            maxExp = 100f;
            expBar.Initialized(maxExp);

            playerLevel = 1;
            ElapsedTime = 0;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) ObjectPool.Instance.GetObject<ExpBall>("ExpBall").transform.position = Vector2.zero;//monsterSpawner.Spawn();
            MapSet();
        }
    }

}
