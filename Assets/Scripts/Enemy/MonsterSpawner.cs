using LMS.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public class MonsterSpawner
    {
        private Transform pTrans;
        private Coroutine coroutine;
        private int maxCommonMonsterCount = 100;
        public MonsterSpawner(Transform pTrans) 
        {
            this.pTrans = pTrans;
            //coroutine = CoroutineManager.Instance.ExecuteCoroutine(AutoSpawn());
        }

        private IEnumerator AutoSpawn()
        {
            int _count = 0;
            int _mounsterCount = (int)(maxCommonMonsterCount * 0.1f);
            while (true)
            {
                if (++_count > 10)
                {
                    _count = 1;
                    maxCommonMonsterCount += (int)(maxCommonMonsterCount * 0.5f);
                    _mounsterCount = (int)(maxCommonMonsterCount * 0.1f);
                }
                int _mCount = maxCommonMonsterCount - CommonMonster.aliveMonsterCount;
                _mounsterCount =  _mCount > _mounsterCount ? _mounsterCount : _mCount;
                Spawn(_mounsterCount);
                yield return Utility.UtilFunctions.WaitForSeconds(3f);
            }
        }
        private float _radius = 10f;
        public void Spawn(int count)
        {
            //for (int _index = 0; _index < MonsterInfo.commonMonsterTypeCount; _index++)
            //{
            //    var _count = Random.Range(0, count + 1);

            //    count -= _count;
            //    CreateCommonMonster(_count, _index);
            //}
            CreateCommonMonster(count, /*MonsterInfo.commonMonsterTypeCount - 1*/0);
        }
        private void CreateCommonMonster(int count, int index)
        {
            if (count == 0) return;
            for (int i = 0; i < count; i++)
            {
                var _deg = Random.Range(0, 360);
                var _x = Mathf.Cos(_deg) * _radius;
                var _y = Mathf.Sin(_deg) * _radius;

                Monster _monster = GetMonster(index);
                if (_monster != null) _monster.transform.position = (Vector2)pTrans.position + new Vector2(_x, _y);
            }
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
