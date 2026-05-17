using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using Zenject;

namespace TheGame
{
    internal class ReturnGroup : MonoBehaviour, IGroupProc
    {
        [Inject]
        private IStorage<GameObject> _boxStorage;//src

        // with simple pool
        public UniTask Process<T>(IGroup<T> group) where T : Component
        {
            for (var i = 0; i < group.Count; i++)
            {
                _boxStorage.Return(group[i].gameObject);
            }
            return UniTask.CompletedTask;
        }
        // PERFORMANCE NOTE
        // if return whole group in pool with Active list
        // Return single item swaps with last, swap can be avoided if simply disable every item
        // first and then set pool Pos to 0
    }
}