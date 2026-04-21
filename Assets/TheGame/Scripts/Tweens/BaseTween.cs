using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;
using TheGame.Common.Animators;

namespace TheGame.Tweens
{
    public class BaseTween : MonoBehaviour, ITween
    {
        [SerializeField] private Wrap<IAnimator> _animator;
        [SerializeField] private int _executeTimeMs = 500;
        [SerializeField] private AnimationCurve _lerpFunc;//transition

        private Func<Vector3, Vector3> _ComputeTargetFunc;
        private AnimationCurveTransition _transition;

        private void Awake()
        {
            _ComputeTargetFunc = ComputeTarget;//alloc
            _transition = new(new(_lerpFunc));
        }

        protected virtual Vector3 ComputeTarget(Vector3 currentPos)
            => currentPos;

        public virtual UniTask Execute<T>(T[] group) where T : Component
            => _animator.Wrappee.AnimateGroup(group, _executeTimeMs, _ComputeTargetFunc, _transition);
    }
}