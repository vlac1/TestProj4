using System.Runtime.InteropServices;
using UnityEngine;

using TheGame.Common.Memory;
using TheGame.Interfaces;

namespace TheGame.Common.Animators
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AnimationCurveCustom<M> : IEvaluate
        where M : unmanaged, IMem
    {
        // unity Keyframe 32bytes, Keyframe2B 12bytes
        private readonly MemView<Keyframe2B, M> _view;

        public AnimationCurveCustom(AnimationCurve curve) : this(curve.keys)
        {
        }

        public AnimationCurveCustom(Keyframe[] keyframes)
        {
            _view = new(keyframes.Length);
            for (int i = 0; i < keyframes.Length; i++)
                _view[i] = keyframes[i];
        }

        public float Evaluate(float time)
        {
            if (_view.Count < 2)
                return 0f;

            // find between 2 frames time is
            for (int i = 0; i < _view.Count - 1; i++)
            {
                var frameThis = _view[i];
                if (time < frameThis.time) continue;

                var frameNext = _view[i + 1];
                if (time > frameNext.time) continue;//last

                return TransitionVal(frameThis, frameNext, time);
            }
            return _view[^1].value;
        }

        private static float TransitionVal(Keyframe2B start, Keyframe2B end, float time)
        {
            float length = end.time - start.time;
            //Mathf.Epsilon
            if (length == 0) return start.value; // fix division by zero

            float t = (time - start.time) / length;

            float startValue = start.value;
            float startTangent = start.Out.Tangent * start.Out.Weight;
            
            float endValue = end.value;
            float endTangent = end.In.Tangent * end.In.Weight;

            // Cubic Bezier
            return (1 - t) * (1 - t) * (1 - t) * startValue +
                   3 * (1 - t) * (1 - t) * t * (startValue + startTangent) +
                   3 * (1 - t) * t * t * (endValue - endTangent) +
                   t * t * t * endValue;
        }
    }

    /*
    // if propagate Keyframes evenly, can find it like this
    time += .00001f;// Mathf.Epsilon;//0 case
    time = Mathf.Clamp01(time) - .00001f;//Mathf.Epsilon;// never last
    var part = 1f / (_view.Count - 1);
    var index = Mathf.FloorToInt(time / part);
    var frame = _view[index];
    var frameNex = _view[index + 1];

    return TransitionVal(frame, frameNex, time);
    //*/
}