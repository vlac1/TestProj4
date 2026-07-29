using System;
using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common
{
    public class CharsToString : MonoBehaviour, ISetChars//SAdapter
    {
        [SerializeField] private Wrap<ISetText> _adaptee;

        public void SetChars(ReadOnlySpan<char> chars)
        {
            var str = chars.ToString();
            _adaptee.Wrappee.SetText(str);
        }
    }
}