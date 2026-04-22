using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace TheGame.Common.Animators
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ControlPoint
    {
        public readonly half Tangent;
        public readonly half Weight;

        public ControlPoint(float tangent, float weight)
        {
            Tangent = (half)tangent;
            Weight = (half)weight;
        }
    }
}