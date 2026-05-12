using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using Zenject;

namespace TheGame
{
    internal class ReturnGroup : MonoBehaviour, ITween
    {
        [Inject]
        private IStorage<GameObject> _boxStorage;//src

        public UniTask Process<T>(IGroup<T> group) where T : Component
        {
            for (var i = 0; i < group.Count; i++)
            {
                _boxStorage.Return(group[i].gameObject);
            }
            return UniTask.CompletedTask;
        }
    }
}