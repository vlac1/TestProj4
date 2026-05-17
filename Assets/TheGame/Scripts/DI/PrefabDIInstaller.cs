using UnityEngine;
using Zenject;

namespace TheGame
{
    internal class PrefabDIInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _boxPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Merger>()
                .FromComponentInNewPrefab(_boxPrefab)
                .AsTransient();
        }
    }
}