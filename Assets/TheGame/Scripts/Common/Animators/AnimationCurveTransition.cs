using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common.Animators
{
    internal struct AnimationCurveTransition<T> : ITransition
        where T : unmanaged, IEvaluate
    {
        private readonly T _curve;

        public AnimationCurveTransition(T curve)
            => _curve = curve;

        public Vector3 Transition(Vector3 a, Vector3 b, float factor)
        {
            var t = _curve.Evaluate(factor);
            return Vector3.Lerp(a, b, t);
        }
    }
}