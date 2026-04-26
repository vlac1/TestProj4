using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common.Animators
{
    internal struct EvaluatorTransition<T> : ITransition
        where T : unmanaged, IEvaluate
    {
        private readonly T _evaluator;

        public EvaluatorTransition(T evaluator)
            => _evaluator = evaluator;

        public Vector3 Transition(Vector3 a, Vector3 b, float factor)
        {
            var t = _evaluator.Evaluate(factor);
            return Vector3.Lerp(a, b, t);
        }
    }
}