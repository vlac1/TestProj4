using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Storages
{
    internal class SimpleGOStorage : MonoBehaviour, IStorage<GameObject>
    {
        [SerializeField] private GameObject _prefab;

        public GameObject Request()
            => Instantiate(_prefab);

        public void Return(GameObject item)
            => Destroy(item);
    }
}