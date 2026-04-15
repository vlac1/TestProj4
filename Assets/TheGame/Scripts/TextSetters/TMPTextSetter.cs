using UnityEngine;
using TMPro;
using TheGame.Interfaces;

namespace TheGame.TextSetters
{
    internal class TMPTextSetter : MonoBehaviour, ISetText
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}