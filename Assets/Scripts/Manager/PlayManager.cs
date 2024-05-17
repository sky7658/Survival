using LMS.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Manager
{
    public class PlayManager : MonoBehaviour
    {
        Enemy.MonsterSpawner ms;
        [SerializeField] private User.Player player;
        private void MapSet()
        {
            Vector2 _pos = transform.position;
            Vector2 _playerPos = player.transform.position;
            float _endX = PlayInfo.mapEndX;
            float _endY = PlayInfo.mapEndY;

            if (_playerPos.x < -_endX) _pos += new Vector2((int)_endX, 0f);
            if (_playerPos.x >= _endX) _pos += new Vector2(-(int)_endX, 0f);
            if (_playerPos.y < -_endY) _pos += new Vector2(0f, (int)_endY);
            if (_playerPos.y >= _endY) _pos += new Vector2(0f, -(int)_endY);

            transform.position = _pos;
        }

        private void Start()
        {
            ms = new Enemy.MonsterSpawner(player.transform);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) ms.Spawn();
            MapSet();
        }
    }

}
