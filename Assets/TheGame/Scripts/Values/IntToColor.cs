using System;
using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Values
{
    public class IntToColor : MonoBehaviour, ISetValue<int>//SAdapter
    {
        [SerializeField] private Wrap<ISetValue<Color>> _adaptee;
        [SerializeField] private Color[] _colors;

        public int Value
        {
            set
            {
                var newColorIndex = Mathf.RoundToInt((float)Math.Log(value, 2)) - 1;
                //Debug.LogFormat("val {0}, ind {1}", pow2Val, newColorIndex);

                newColorIndex = Mathf.Clamp(newColorIndex, 0, _colors.Length - 1);
                var newColor = _colors[newColorIndex];
                _adaptee.Wrappee.Value = newColor;
            }
        }
    }
}