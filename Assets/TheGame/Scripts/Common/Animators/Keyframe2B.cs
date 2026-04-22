using System.Runtime.InteropServices;
using UnityEngine;
using Unity.Mathematics;

namespace TheGame.Common.Animators
{
    // can use less mem, 2byte float, fit more in Mem128
    [StructLayout(LayoutKind.Sequential)]
    internal struct Keyframe2B
    {
        public readonly half time;
        public readonly half value;

        public readonly ControlPoint In;
        public readonly ControlPoint Out;

        public static implicit operator Keyframe2B(Keyframe unity)
            => new(unity);

        public Keyframe2B(Keyframe unity)
        {
            time = (half)unity.time;
            value = (half)unity.value;
            In = new(unity.inTangent, unity.inWeight);
            Out = new(unity.outTangent, unity.outWeight);
        }
    }
}