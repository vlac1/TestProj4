using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    internal class SimpleEntityGroupProvider : MonoBehaviour, ITween, IArrayish<BoxEntity>
    {
        public BoxEntity this[int index] => _entities[index];

        public int Count => _entities.Length;

        private BoxEntity[] _entities;

        private void OnDestroy()
        {
            _entities = null;
        }

        // put this first in list of Tweens, this used as init
        public UniTask Execute<T>(IArrayish<T> items) where T : Component
        {
            // TODO or use pool active list
            _entities = FindObjectsByType<BoxEntity>(FindObjectsSortMode.None);
            foreach (var box in _entities)
            {
                box.Rigidbody.isKinematic = true;
                box.Rigidbody.detectCollisions = false;
            }
            return UniTask.CompletedTask;
        }
    }
}