using UnityEngine;
using Zenject;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class BoxStorageInstaller : MonoInstaller
    {
        [SerializeField] private Wrap<IStorage<GameObject>> _boxStorage;

        public override void InstallBindings()
        {
            Container.Bind<IStorage<GameObject>>()
                .To<IStorage<GameObject>>()
                .FromInstance(_boxStorage.Wrappee).AsTransient();
        }
    }
}