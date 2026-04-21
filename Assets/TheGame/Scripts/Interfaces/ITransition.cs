using UnityEngine;

namespace TheGame.Interfaces
{
    internal interface ITransition//<V>
    {
        public Vector3 Transition(Vector3 a, Vector3 b, float factor);
    }
}