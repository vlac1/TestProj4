using UnityEngine;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Storages.Pools
{
    internal class PoolGOStorage : PoolMono<GameObject>
    {
        [SerializeField] protected Wrap<IStorage<GameObject>> _simplerStorage;

        private void Awake()
        {
            //_pool = new PoolWithActives<BoxEntity>(_preWarm, Factrory);
            _pool = new QueuePool<GameObject>(_preWarm, Factrory);
        }

        protected override GameObject Factrory()
        {
            var newItem = _simplerStorage.Wrappee.Request();
            //newItem.GetComponent<BoxEntity>();
            newItem.SetActive(false);
            return newItem;
        }

        public override GameObject Request()
        {
            var item = base.Request();
            item.SetActive(true);
            return item;
        }

        public override void Return(GameObject item)
        {
            // reset item state
            item.SetActive(false);
            item.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            var rig = item.GetComponent<Rigidbody>();
            rig.velocity = Vector3.zero;
            rig.isKinematic = false;
            rig.detectCollisions = true;
            base.Return(item);
        }
    }
}