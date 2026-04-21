using System;
using System.Runtime.InteropServices;
using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common.Memory;

namespace TheGame.Common.Animators
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AnimationCurveCustom//<M>
        //where M : IMem
    {
        //Keyframe 32byte
        private readonly MemView<Keyframe, Mem128> _view;//M

        public AnimationCurveCustom(AnimationCurve curve) : this(curve.keys)
        {
        }

        public AnimationCurveCustom(Keyframe[] keyframes)
        {
            if (keyframes.Length > 4)
                throw new Exception("Mem CANT store more Keyframes");

            _view = new(keyframes.Length);
            for (int i = 0; i < keyframes.Length; i++)
                _view[i] = keyframes[i];
        }

        public float Evaluate(float time)
        {
            if (_view.Count < 2)
                return 0f;

            // find 2 frames in time
            for (int i = 0; i < _view.Count - 1; i++)
            {
                var frameThis = _view[i];
                if (time < frameThis.time) continue;

                var frameNext = _view[i + 1];
                if (time > frameNext.time) continue;

                return TransitionVal(frameThis, frameNext, time);
            }
            return _view[^1].value;
        }

        private static float TransitionVal(Keyframe start, Keyframe end, float time)
        {
            float length = end.time - start.time;
            //Mathf.Epsilon
            if (length == 0) return start.value; // fix division by zero

            float t = (time - start.time) / length;

            float startValue = start.value;
            float startTangent = start.outTangent * start.outWeight;
            
            float endValue = end.value;
            float endTangent = end.inTangent * end.inWeight;

            // Cubic Bezier
            return (1 - t) * (1 - t) * (1 - t) * startValue +
                   3 * (1 - t) * (1 - t) * t * (startValue + startTangent) +
                   3 * (1 - t) * t * t * (endValue - endTangent) +
                   t * t * t * endValue;
        }
    }

    // TODO? can use less mem, 2byte float, fit more in Mem128
    struct KeyframeS
    {
        public float time { get; set; }//Float2B
        public float value { get; set; }

        public ControlPoint In { get; set; }
        public ControlPoint Out { get; set; }
    }

    struct ControlPoint
    {
        public float Tangent { get; set; }
        public float Weight { get; set; }
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