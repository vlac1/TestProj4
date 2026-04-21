using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common.Animators
{
    internal struct Vector3Lerp : ITransition
    {
        public Vector3 Transition(Vector3 a, Vector3 b, float factor)
            => Vector3.Lerp(a, b, factor);
    }
}