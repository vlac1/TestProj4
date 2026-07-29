using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Values
{
    public class IntValChangedSet : MonoBehaviour, IValue<int>//SDecorator
    {
        [SerializeField] private Wrap<IGetValue<int>> _valGet;
        [SerializeField] private Wrap<ISetValue<int>> _valSet;

        public int Value
        {
            get => _valGet.Wrappee.Value;//currentVal
            set
            {
                if (Value != value)
                    _valSet.Wrappee.Value = value;
            }
        }
    }
}