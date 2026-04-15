using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.TextSetters
{
    internal class TextSetters : MonoBehaviour, ISetText//arr dec
    {
        [SerializeField] private Wrap<ISetText>[] _textSetters;

        public void SetVal(int val)//OR int to string conv
        {
            SetText(val.ToString());
        }

        public void SetText(string text)
        {
            for (var i = 0; i < _textSetters.Length; i++)
            {
                _textSetters[i].Wrappee.SetText(text);
            }
        }
    }
}