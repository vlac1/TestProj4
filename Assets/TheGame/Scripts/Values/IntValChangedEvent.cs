using UnityEngine;
using UnityEngine.Events;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Values
{
    public class IntValChangedEvent : MonoBehaviour, IValue<int>
    {
        [SerializeField] private Wrap<IValue<int>> _observedVal;
        [SerializeField] private UnityEvent<int> _onValChanged;

        public int Value {
            get => _observedVal.Wrappee.Value;
            set => SetValue(value); }

        private void SetValue(int newVal)
        {
            var observed = _observedVal.Wrappee;

            var oldVal = observed.Value;
            observed.Value = newVal;
            // read it back, it might didn't changed
            var updatedVal = observed.Value;
            if (oldVal != updatedVal)
                _onValChanged.Invoke(updatedVal);
        }
    }
}