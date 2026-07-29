using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.TextSetters
{
    internal class TextSetters : MonoBehaviour, ISetText//SComposite
    {
        [SerializeField] private Wrap<ISetText>[] _textSetters;


        //IntValToSetText
        public void SetVal(int val)//OR int to string SAdapter
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