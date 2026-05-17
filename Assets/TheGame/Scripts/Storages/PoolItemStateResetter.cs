using UnityEngine;
using TheGame.Interfaces;
using Zenject;
using Cysharp.Threading.Tasks;

namespace TheGame.Storages
{
    // this no need if item created and destroyed every time
    // for single item OR as group
    internal class PoolItemStateResetter : MonoBehaviour
        , IStorage<GameObject>
        , IGroupProc
    {
        [Inject(Id = "Pool")]
        private IStorage<GameObject> _pool;

        public UniTask Process<T>(IGroup<T> group) where T : Component
        {
            for (var i = 0; i < group.Count; i++)
                Disable(group[i].gameObject);
            return UniTask.CompletedTask;
        }

        public GameObject Request()
        {
            var item = _pool.Request();
            item.SetActive(true);
            return item;
        }

        public void Return(GameObject item)
        {
            Disable(item);
            _pool.Return(item);
        }

        // could use this on Prefab
        // but will run every time GO disabled, and what if this behav no need
        private void Disable(GameObject item)
        {
            // reset item state
            item.SetActive(false);
            item.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            var rig = item.GetComponent<Rigidbody>();
            rig.velocity = Vector3.zero;
            rig.isKinematic = false;
            rig.detectCollisions = true;
        }
    }
}