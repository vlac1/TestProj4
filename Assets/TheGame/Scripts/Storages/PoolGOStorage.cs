using UnityEngine;
using Zenject;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal class PoolGOStorage : MonoInstaller
    {
        [SerializeField] private int _preWarm;
        [SerializeField] private Wrap<IStorage<GameObject>> _simplerStorage;
        [SerializeField] private GameObject _prefab;

        private QueuePool<GameObject> _pool;

        public override void InstallBindings()
        {
            //todo exec sequence
            _pool = new QueuePool<GameObject>(Factrory);//, _preWarm

            Container.Bind<IStorage<GameObject>>()
                .WithId("Pool").FromInstance(_pool).AsTransient();
        }

        private GameObject Factrory()
        {
            var newItem = Container.InstantiatePrefab(_prefab);
            //var newItem = _simplerStorage.Wrappee.Request();
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