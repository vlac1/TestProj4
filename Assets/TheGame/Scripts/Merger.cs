using UnityEngine;
using Zenject;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class Merger : MonoBehaviour
    {
        [SerializeField] private float _minImpulse = 1;
        [SerializeField] private Wrap<IValue<int>> _boxVal;

        private IValue<int> Score => _boxVal.Wrappee;

        [Inject]
        private IStorage<GameObject> _boxProvider;

        private void OnCollisionEnter(Collision collision)
        {
            var isHardEnough = _minImpulse * _minImpulse < collision.impulse.sqrMagnitude;
            if (!isHardEnough) return;

            var collidedR = collision.rigidbody;
            if (collidedR == null || !collidedR.TryGetComponent(out IValue<int> hittedBoxVal)) return;

            // merge if same val
            var isSameVal = hittedBoxVal.Value == Score.Value;
            if (!isSameVal) return;

            Score.Value += hittedBoxVal.Value;

            // return fast moving back, as if it run into standing still and merged with it
            _boxProvider.Return(collidedR.gameObject);
        }
    }
}