using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal abstract class MonoPool<P, I> : MonoBehaviour
        where P : IStorage<I>, IPrewarm
    {
        [SerializeField] private int _preWarm;
        [SerializeField] private GameObject _prefab;

        protected P _pool;
        protected abstract P PoolFactory();

        private void Awake()// after InstallBindings
        {
            _pool.Prewarm(_preWarm);
        }
    }
}