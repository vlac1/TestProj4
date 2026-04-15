using UnityEngine;

namespace TheGame.Storages
{
    internal class PoolGOStorage : QueuePoolMono<GameObject>
    {
        protected override GameObject Factrory()
        {
            var newItem = _simplerStorage.Wrappee.Request();
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