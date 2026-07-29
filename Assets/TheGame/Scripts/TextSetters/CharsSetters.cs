using System;
using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.TextSetters
{
    internal class CharsSetters : MonoBehaviour, ISetChars//SComposite
    {
        [SerializeField] private Wrap<ISetChars>[] _charsSetters;

        public void SetChars(ReadOnlySpan<char> chars)
        {
            for (var i = 0; i < _charsSetters.Length; i++)
            {
                _charsSetters[i].Wrappee.SetChars(chars);
            }
        }
    }
}