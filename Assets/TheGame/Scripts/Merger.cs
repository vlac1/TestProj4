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

        public IValue<int> Score => _boxVal.Wrappee;

        [Inject]
        private IStorage<GameObject> _boxProvider;

        private void OnCollisionEnter(Collision collision)
        {
            var isHardEnough = _minImpulse * _minImpulse < collision.impulse.sqrMagnitude;
            if (!isHardEnough) return;

            var hitted = collision.rigidbody;
            if (hitted == null || !hitted.TryGetComponent(out IValue<int> hittedBoxVal)) return;

            // merge if same val
            var isSameVal = hittedBoxVal.Value == _boxVal.Wrappee.Value;
            if (!isSameVal) return;

            _boxVal.Wrappee.Value += hittedBoxVal.Value;

            // return fast moving back, as if it run into standing still and merged with it
            _boxProvider.Return(hitted.gameObject);
        }
    }
}