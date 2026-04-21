using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Tweens
{
    public class BaseTween : MonoBehaviour, ITween
    {
        [SerializeField] private Wrap<IAnimator> _animator;
        [SerializeField] private int _executeTimeMs = 500;
        [SerializeField] private AnimationCurve _lerpFunc;

        private Func<Vector3, Vector3, float, Vector3> _LerpFunc;
        private Func<Vector3, Vector3> _ComputeTargetFunc;

        private void Awake()
        {
            _LerpFunc = LerpFunc;//allocs
            _ComputeTargetFunc = ComputeTarget;
        }

        protected virtual Vector3 LerpFunc(Vector3 a, Vector3 b, float t)
        {
            t = _lerpFunc.Evaluate(t);//non linear
            return Vector3.Lerp(a, b, t);
        }

        protected virtual Vector3 ComputeTarget(Vector3 currentPos)
            => currentPos;

        public virtual UniTask Execute<T>(T[] boxes) where T : Component
            => _animator.Wrappee.AnimateGroup(boxes, _executeTimeMs, _ComputeTargetFunc, _LerpFunc);
    }
}