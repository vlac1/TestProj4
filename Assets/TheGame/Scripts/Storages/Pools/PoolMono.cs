using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Storages
{
    internal abstract class PoolMono<TItem> : MonoBehaviour, IStorage<TItem>
    {
        [SerializeField] protected int _preWarm;

        protected IStorage<TItem> _pool;

        protected abstract TItem Factrory();

        public virtual TItem Request()
            => _pool.Request();

        public virtual void Return(TItem item)
            => _pool.Return(item);
    }
}