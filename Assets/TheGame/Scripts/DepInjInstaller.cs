using UnityEngine;
using Zenject;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class DepInjInstaller : MonoInstaller
    {
        [SerializeField] private Wrap<IStorage<GameObject>> _boxStorage;
        [SerializeField] private GameObject _boxPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IStorage<GameObject>>()
                .To<IStorage<GameObject>>()
                .FromInstance(_boxStorage.Wrappee)
                .AsSingle();

            Container.Bind<Merger>()
            .FromComponentInNewPrefab(_boxPrefab)
            .AsTransient();
        }
    }
}