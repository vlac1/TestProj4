using UnityEngine;

namespace TheGame.Common
{
    internal class RigidbodyLauncher : MonoBehaviour
    {
        [SerializeField] private float _impulse;
        [SerializeField] private Transform _dir;

        public void Launch(Rigidbody rigidbody)
        {
            var imp = Vector3.forward * _impulse;
            rigidbody.AddForce(imp, ForceMode.Impulse);
        }
    }
}