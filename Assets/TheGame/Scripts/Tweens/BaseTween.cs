using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

using TheGame.Interfaces;
using TheGame.Common;
using TheGame.Common.Memory;
using TheGame.Common.Animators;

namespace TheGame.Tweens
{
    public class BaseTween : MonoBehaviour, ITween
    {
        [SerializeField] private Wrap<IAnimator> _animator;//or each can have its own
        [SerializeField] private int _executeTimeMs = 500;
        [SerializeField] private AnimationCurve _curve;

        //[Inject]
        //private IAnimator _animator;
        private Func<Vector3, Vector3> _ComputeTargetFunc;
        private EvaluatorTransition<AnimationCurveCustom<Mem64>> _transition;

        private void Awake()
        {
            _ComputeTargetFunc = ComputeTarget;//alloc
            _transition = new(new(_curve));
        }

        protected virtual Vector3 ComputeTarget(Vector3 currentPos)
            => currentPos;

        public virtual UniTask Execute<T>(T[] group) where T : Component
            => _animator.Wrappee.AnimateGroup(group, _executeTimeMs, _ComputeTargetFunc, _transition);
    }
}