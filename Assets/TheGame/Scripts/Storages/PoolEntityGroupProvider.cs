using UnityEngine;
using TheGame.Interfaces;
using TheGame.Storages.Pools;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    // SimpleEntityGroupProvider v2
    //BoxEntity
    internal class PoolEntityGroupProvider : PoolInstaller<PoolWithActives<BoxEntity>, BoxEntity>
        , IGroup<BoxEntity>
        , IGroupProc//to change pos=0
    {
        public BoxEntity this[int index] => _pool[index];

        public int Count => _pool.PoolPos;

        protected override PoolWithActives<BoxEntity> PoolFactory()
            => new(FactroryItem);

        protected BoxEntity FactroryItem()
            => PrefabFactrory().GetComponent<BoxEntity>();

        public UniTask Process<T>(IGroup<T> group) where T : Component
        {
            _pool.SetPos0();
            return UniTask.CompletedTask;
        }
    }
}