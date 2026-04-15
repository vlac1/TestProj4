using System;
using UnityEngine;

namespace TheGame.Common
{
    internal class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color[] _colors;

        private MaterialPropertyBlock _materialProperty;

        private void Awake()
        {
            _materialProperty = new();
            _renderer.GetPropertyBlock(_materialProperty);
        }

        public void ChangeColor(int pow2Val)
        {
            var newColorIndex = Mathf.RoundToInt((float)Math.Log(pow2Val, 2)) - 1;
            //Debug.LogFormat("val {0}, ind {1}", pow2Val, newColorIndex);

            newColorIndex = Mathf.Clamp(newColorIndex, 0, _colors.Length-1);
            var newColor = _colors[newColorIndex];
            _materialProperty.SetColor(IDs._ColorID, newColor);
            _renderer.SetPropertyBlock(_materialProperty);
        }
    }

    internal static class IDs
    {
        public static readonly int _ColorID = Shader.PropertyToID("_Color");
    }
}