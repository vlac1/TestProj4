using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common
{
    internal class ColorChanger : MonoBehaviour, ISetValue<Color>
    {
        [SerializeField] private Renderer _renderer;

        private MaterialPropertyBlock _materialProperty;

        private void Awake()
        {
            _materialProperty = new();
            _renderer.GetPropertyBlock(_materialProperty);
        }

        public Color Value
        {
            set
            {
                _materialProperty.SetColor(IDs._ColorID, value);
                _renderer.SetPropertyBlock(_materialProperty);
            }
        }
    }

    internal static class IDs
    {
        public static readonly int _ColorID = Shader.PropertyToID("_Color");
    }
}