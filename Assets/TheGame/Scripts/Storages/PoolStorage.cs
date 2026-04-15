using UnityEngine;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Storages
{
    internal abstract class QueuePoolMono<TItem> : MonoBehaviour, IStorage<TItem>
    {
        [SerializeField] protected int _preWarm;
        [SerializeField] protected Wrap<IStorage<TItem>> _simplerStorage;

        private QueuePool<TItem> _pool;

        protected abstract TItem Factrory();

        private void Awake()
        {
            _pool = new(_preWarm, Factrory);
        }

        public virtual TItem Request()
            => _pool.Get();

        public virtual void Return(TItem item)
            => _pool.Put(item);
    }
}