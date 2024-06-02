using LMS.Manager;
using System.Collections;
using UnityEngine;

namespace LMS.Enemy
{
    public class MonsterSpawner
    {
        private Transform pTrans;
        private Coroutine coroutine;
        private int maxCommonMonsterCount = 100;
        public static readonly float _radius = 10f;

        public MonsterSpawner(Transform pTrans) 
        {
            this.pTrans = pTrans;
            StartCommonMonsterSpawn();
        }
        public void StartCommonMonsterSpawn() => coroutine = CoroutineManager.Instance.ExecuteCoroutine(AutoSpawn());
        public void StopCommonMonsterSpawn() => CoroutineManager.Instance.QuitCoroutine(coroutine);
        private IEnumerator AutoSpawn()
        {
            int _count = 0;
            int _mounsterCount = (int)(maxCommonMonsterCount * 0.05f);
            while (true)
            {
                if (!PlayManager.Instance.IsGamePlay) yield return null;
                else
                {
                    if (PlayManager.Instance.BossStage)
                    {
                        var _deg = Random.Range(0, 360);
                        var _boss = GetMonster(4);
                        _boss.transform.position = (Vector2)pTrans.position 
                            + new Vector2(Mathf.Cos(_deg * Mathf.Deg2Rad) * (_radius + 2f), Mathf.Sin(_deg * Mathf.Deg2Rad) * (_radius + 2f));

                        CutSceneManager.Instance.StartBossModeCutScene(_boss.transform.position - pTrans.position);
                        yield break;
                    }
                    if (++_count > 20)
                    {
                        _count = 1;
                        maxCommonMonsterCount += (int)(maxCommonMonsterCount * 0.5f);
                        _mounsterCount = (int)(maxCommonMonsterCount * 0.05f);
                    }
                    int _mCount = maxCommonMonsterCount - CommonMonster.aliveMonsterCount;
                    _mounsterCount = _mCount > _mounsterCount ? _mounsterCount : _mCount;
                    Spawn(_mounsterCount);
                    yield return Utility.UtilFunctions.WaitForSeconds(3f);
                }
            }
        }
        public void Spawn(int count)
        {
            for (int _index = 0; _index < MonsterInfo.commonMonsterTypeCount; _index++)
            {
                var _count = Random.Range(0, count + 1);

                count -= _count;
                CreateMonster(_count, _index);
            }
            CreateMonster(count, MonsterInfo.commonMonsterTypeCount - 1);
        }
        private void CreateMonster(int count, int index)
        {
            if (count == 0) return;
            for (int i = 0; i < count; i++)
            {
                var _range = StartDegree();
                float _deg;

                if (_range == default(float))
                    _deg = Random.Range(0, 360);
                else
                    _deg = Random.Range(_range - 45, _range + 45);

                var _x = Mathf.Cos(_deg * Mathf.Deg2Rad) * _radius;
                var _y = Mathf.Sin(_deg * Mathf.Deg2Rad) * _radius;

                Monster _monster = GetMonster(index);
                if (_monster != null) _monster.transform.position = (Vector2)pTrans.position + new Vector2(_x, _y);
            }
        }
        private float StartDegree()
        {
            if (!Controller.InputManager.isMoveKeyDown()) return default(float);
            Vector2 _moveV = Controller.InputManager.moveVector.normalized;
            return Mathf.Atan2(_moveV.y, _moveV.x) * Mathf.Rad2Deg;
        }
        private Monster GetMonster(int index)
        {
            switch (index)
            {
                case 0:
                    return Utility.ObjectPool.Instance.GetObject<Bat>(MonsterInfo.mnameSO.Bat);
                case 1:
                    return Utility.ObjectPool.Instance.GetObject<Crab>(MonsterInfo.mnameSO.Crab);
                case 2:
                    return Utility.ObjectPool.Instance.GetObject<Pebble>(MonsterInfo.mnameSO.Pebble);
                case 3:
                    return Utility.ObjectPool.Instance.GetObject<Slime>(MonsterInfo.mnameSO.Slime);
                case 4:
                    return Utility.ObjectPool.Instance.GetObject<Golem>(MonsterInfo.mnameSO.Golem);
            }
            return null;
        }
    }
}
