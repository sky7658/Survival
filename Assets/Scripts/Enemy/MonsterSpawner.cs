using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LMS.Enemy
{
    public class MonsterSpawner
    {
        private Transform pTrans;
        private int CountMultiply
        {
            get
            {
                return 1;
            }
        }
        public MonsterSpawner(Transform pTrans) { this.pTrans = pTrans; }

        private float _radius = 5f;
        public void Spawn()
        {
            int _allRatio = 10;
            int[] _ratios = new int[MonsterInfo.commonMonsterTypeCount];

            for (int _index = 0; _index < _ratios.Length - 1; _index++)
            {
                var _ratio = Random.Range(0, _allRatio + 1);

                _ratios[_index] = _ratio;
                _allRatio -= _ratio;

                CreateCommonMonster(_ratio, _index + 1);
            }
            CreateCommonMonster(_allRatio * CountMultiply, _ratios.Length);
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
                case 1:
                    return Utility.ObjectPool.Instance.GetObject<Bat>(MonsterInfo.mnameSO.Bat);
                case 2:
                    return Utility.ObjectPool.Instance.GetObject<Crab>(MonsterInfo.mnameSO.Crab);
                case 3:
                    return Utility.ObjectPool.Instance.GetObject<Pebble>(MonsterInfo.mnameSO.Pebble);
                case 4:
                    return Utility.ObjectPool.Instance.GetObject<Slime>(MonsterInfo.mnameSO.Slime);
                case 5:
                    return Utility.ObjectPool.Instance.GetObject<Golem>(MonsterInfo.mnameSO.Golem);
            }

            return null;
        }
    }
}
