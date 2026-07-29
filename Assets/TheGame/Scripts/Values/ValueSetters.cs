using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Values
{
    internal class ValueSetters<T> : MonoBehaviour, ISetValue<T>//SComposite
    {
        [SerializeField] private Wrap<ISetValue<T>>[] _setters;

        public T Value
        {
            set
            {
                for (var i = 0; i < _setters.Length; i++)
                {
                    _setters[i].Wrappee.Value = value;
                }
            }
        }
    }
}