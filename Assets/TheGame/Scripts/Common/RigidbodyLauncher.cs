using UnityEngine;

namespace TheGame.Common
{
    internal class RigidbodyLauncher : MonoBehaviour
    {
        [SerializeField] private float _impulse;
        [SerializeField] private Transform _dir;

        public void Launch(Rigidbody rigidbody)
        {
            var dir = _dir.forward;
            var imp = dir * _impulse;
            rigidbody.AddForce(imp, ForceMode.Impulse);
        }
    }
}