using UnityEngine;
using TheGame.Interfaces;
using Zenject;

namespace TheGame.Storages
{
    // this no need if item created and destroyed every time
    internal class PoolItemStateResetter : MonoBehaviour, IStorage<GameObject>
    {
        [Inject(Id = "Pool")]
        private IStorage<GameObject> _pool;

        public GameObject Request()
        {
            var item = _pool.Request();
            item.SetActive(true);
            return item;
        }

        public void Return(GameObject item)
        {
            // reset item state
            item.SetActive(false);
            item.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            var rig = item.GetComponent<Rigidbody>();
            rig.velocity = Vector3.zero;
            rig.isKinematic = false;
            rig.detectCollisions = true;
            _pool.Return(item);
        }
    }
}