using UnityEngine;
using Zenject;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal abstract class PoolInstaller<P,I> : MonoInstaller
        where P : IStorage<I>, IPrewarm
    {
        [SerializeField] private int _preWarm;
        [SerializeField] private GameObject _prefab;

        protected P _pool;
        protected abstract P Factory();

        public override void InstallBindings()
        {
            //todo exec sequence, cant use prewarm here as Container in FactroryItem not ready
            // set to 0 here, moved to Awake
            _pool = Factory();
            //_pool = new QueuePool<GameObject>(FactroryItem);//, _preWarm

            Container.Bind<IStorage<I>>()
                .WithId("Pool").FromInstance(_pool).AsTransient();
        }

        protected virtual GameObject FactroryPrefab()
        {
            var newItem = Container.InstantiatePrefab(_prefab);
            //newItem.GetComponent<BoxEntity>();
            newItem.SetActive(false);
            return newItem;
        }

        private void Awake()// after InstallBindings
        {
            _pool.Prewarm(_preWarm);
        }
    }
}