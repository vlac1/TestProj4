using UnityEngine;
using TheGame.Interfaces;

using Unity.Mathematics;
using Unity.Collections;
using UnityEngine.Jobs;

namespace TheGame.Common.Animators
{
    internal struct PositionUpdateJob<T> : IJobParallelForTransform
        where T : unmanaged, ITransition
    {
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<float3> Starts;
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<float3> Targets;

        public T Transition;
        public float Factor;

        public void Execute(int index, TransformAccess transform)
        {
            var start = Starts[index];
            var target = Targets[index];
            var t = Mathf.Clamp01(Factor);
            transform.position = Transition.Transition(start, target, t);
        }
    }
}