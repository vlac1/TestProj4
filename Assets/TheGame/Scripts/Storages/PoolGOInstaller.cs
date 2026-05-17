using UnityEngine;

namespace TheGame.Storages.Pools
{
    internal class PoolGOInstaller : PoolInstaller<QueuePool<GameObject>, GameObject>
    {
        protected override QueuePool<GameObject> Factory()
            => new(FactroryPrefab);
    }
}