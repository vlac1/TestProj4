using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class BoxEntity : MonoBehaviour, IId<int>// E (ECS)
    {
        [SerializeField] private Wrap<IValue<int>> _boxVal;

        public int Id { get; set; }
        public IValue<int> Score => _boxVal.Wrappee;
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    }
}