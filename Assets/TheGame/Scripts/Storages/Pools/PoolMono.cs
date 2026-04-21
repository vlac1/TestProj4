using UnityEngine;
using TheGame.Interfaces;
using Zenject;

namespace TheGame.Storages
{
    internal abstract class PoolMono<TItem> : MonoBehaviour, IStorage<TItem>
    {
        [SerializeField] protected int _preWarm;

        //[Inject(Id = "Pool")]
        protected IStorage<TItem> _pool;

        protected abstract TItem Factrory();

        public virtual TItem Request()
            => _pool.Request();

        public virtual void Return(TItem item)
            => _pool.Return(item);
    }
}