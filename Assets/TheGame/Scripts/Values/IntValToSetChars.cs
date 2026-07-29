using System;
using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Values
{
    internal class IntValToSetChars : MonoBehaviour, ISetValue<int>//SAdapter
    {
        [SerializeField] private Wrap<ISetChars> _adaptee;

        private ISetChars Adaptee => _adaptee.Wrappee;

        public int Value
        {
            set
            {
                Span<char> buffer = stackalloc char[32];
                var isOk = value.TryFormat(buffer, out int charsWritten);
                Adaptee.SetChars(isOk ? buffer.Slice(0, charsWritten) : "N/A".AsSpan());
            }
        }
    }
}