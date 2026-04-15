using System;
using UnityEngine;

namespace TheGame.Common
{
    [Serializable]
    public class Wrap<T> : ISerializationCallbackReceiver
         where T : class
    {
        [SerializeField] private Behaviour _wrappee;

        public T Wrappee => _wrappee as T;

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            //TODO dont allow itself, as it can STUCK in endless loop
            if (_wrappee is not T)
                _wrappee = null;
        }
    }
}