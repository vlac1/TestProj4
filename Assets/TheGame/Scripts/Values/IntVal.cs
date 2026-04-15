using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Values
{
    public class IntVal : MonoBehaviour, IValue<int>
    {
        [field: SerializeField] public int Value { get; set; }
    }
}