using System;
using UnityEngine;
using UnityEngine.Profiling;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;

using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;

namespace TheGame.Common
{
    using LerpFunc = Func<Vector3, Vector3, float, Vector3>;

    public class JobsAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField] private int _maxItems = 200;

        private NativeArray<Vector3> _starts;
        private NativeArray<Vector3> _targets;
        private TransformAccessArray _transforms;

        #region unity
        private void Awake()
        {
            Profiler.BeginSample("job data");
            _starts = new(_maxItems, Allocator.Persistent);
            _targets = new(_maxItems, Allocator.Persistent);
            _transforms = new(_maxItems);
            Profiler.EndSample();
        }

        private void OnDestroy()
        {
            _starts.Dispose();
            _targets.Dispose();
            if(_transforms.isCreated)
                _transforms.Dispose();
        }
        #endregion

        public async UniTask AnimateGroup<T>(T[] items, int ms, Func<Vector3, Vector3> computeTarget, LerpFunc func)
            where T : Component
        {
            //clear
            while (_transforms.length > 0)
                _transforms.RemoveAtSwapBack(0);

            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                var itemT = item.transform;
                var startPos = itemT.position;
                var targetPos = computeTarget(startPos);

                _transforms.Add(itemT);
                _starts[i] = startPos;
                _targets[i] = targetPos;
            }

            //TODO func, anim "flat"
            PositionUpdateJob<Func01> _positionUpdateJob = new ()
            {
                Factor = 0,
                Starts = _starts,
                Targets = _targets,
                //_func = func
            };

            var duration = ms / 1000f;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                _positionUpdateJob.Factor = t;
                var jobHandle = _positionUpdateJob.Schedule(_transforms);
                await jobHandle.WaitAsync(PlayerLoopTiming.EarlyUpdate);
                //jobHandle.Complete();
            }
            //_transforms.Dispose();
        }
    }

    interface IFuncWrap
    {
        public Vector3 Lerp(Vector3 a, Vector3 b, float factor);
    }

    struct Func01 : IFuncWrap
    {
        public Vector3 Lerp(Vector3 a, Vector3 b, float factor)
            => Vector3.Lerp(a, b, factor);
    }

    /*
    struct Func02 : IFuncWrap
    {
        //[SerializeField] private AnimationCurveCustom _curve;

        public Vector3 Lerp(Vector3 a, Vector3 b, float factor)
            => Vector3.Lerp(a, b, factor);
    }//*/

    internal struct PositionUpdateJob<T> : IJobParallelForTransform
        where T : unmanaged, IFuncWrap
    {
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<Vector3> Starts;
        [NativeDisableParallelForRestriction] [ReadOnly] public NativeArray<Vector3> Targets;

        public T _func;
        public float Factor;

        public void Execute(int index, TransformAccess transform)
        {
            var start = Starts[index];
            var target = Targets[index];
            var t = Mathf.Clamp01(Factor);
            transform.position = _func.Lerp(start, target, t);
        }
    }
}