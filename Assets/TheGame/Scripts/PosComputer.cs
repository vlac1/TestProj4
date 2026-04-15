using UnityEngine;

namespace TheGame
{
    internal class PosComputer : MonoBehaviour
    {
        [SerializeField] private Transform _controlPointA;
        [SerializeField] private Transform _controlPointB;

        public Vector3 GetPos(float t)
        {
            var cA = _controlPointA.position;
            var cB = _controlPointB.position;
            return Vector3.Lerp(cA, cB, t);
        }
    }
}