using UnityEngine;
using TheGame.Interfaces;
using TheGame.Storages.Pools;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    // SimpleEntityGroupProvider v2
    internal class PoolEntityGroupProvider : MonoBehaviour, IGroup<BoxEntity>
    {
        public BoxEntity this[int index] => _pool[index];

        public int Count => _pool.PoolPos;

        PoolWithActives<BoxEntity> _pool;

        private void Awake()
        {
            //_pool = new PoolWithActives<BoxEntity>(_preWarm, Factrory);

            // prewarm
            // inject
        }
    }
}