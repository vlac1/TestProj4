using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    internal class SimpleEntityGroupProvider : MonoBehaviour, ITween, IGroup<BoxEntity>
    {
        public BoxEntity this[int index] => _entities[index];

        public int Count => _entities.Length;

        private BoxEntity[] _entities;

        private void OnDestroy()
        {
            _entities = null;
        }

        // put this first in list of Tweens, this used as init
        public UniTask Process<T>(IGroup<T> items) where T : Component
        {
            // TODO or use pool active list
            _entities = FindObjectsByType<BoxEntity>(FindObjectsSortMode.None);
            foreach (var box in _entities)
            {
                box.Rigidbody.isKinematic = true;
                box.Rigidbody.detectCollisions = false;
            }
            //or use
            //Physics.OverlapSphereNonAlloc();
            return UniTask.CompletedTask;
        }
    }
}