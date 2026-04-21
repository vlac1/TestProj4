using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common.Animators
{
    internal struct AnimationCurveTransition : ITransition
    {
        private readonly AnimationCurveCustom _curve;

        public AnimationCurveTransition(AnimationCurveCustom curve)
            => _curve = curve;

        public Vector3 Transition(Vector3 a, Vector3 b, float factor)
        {
            var t = _curve.Evaluate(factor);
            return Vector3.Lerp(a, b, t);
        }
    }
}