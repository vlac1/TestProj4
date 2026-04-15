using UnityEngine;
using Zenject;
using TheGame.Interfaces;

namespace TheGame.Storages
{
    internal class ZenjectGOStorage : MonoBehaviour, IStorage<GameObject>
    {
        [SerializeField] private GameObject _prefab;
        
        [Inject]
        private DiContainer _container;

        public GameObject Request()
            => _container.InstantiatePrefab(_prefab);

        public void Return(GameObject item)
            => Destroy(item);
    }
}