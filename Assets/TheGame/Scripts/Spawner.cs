using UnityEngine;
using Zenject;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class Spawner : MonoBehaviour, IState
    {
        [SerializeField] private PosComputer _posComputer;
        [SerializeField] private RandNumber _randNumber;
        [SerializeField] private RigidbodyLauncher _launcher;

        [Inject]
        private IStorage<GameObject> _boxStorage;//
        private Transform _current;

        #region unity
        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            var t = Input.mousePosition.x / Screen.width;
            var pos = _posComputer.GetPos(t);

            _current.position = pos;
        }
        #endregion

        public void Began()
        {
            var spawnPos = _posComputer.GetPos(.5f);//at center
            _current = _boxStorage.Request().transform;
            _current.position = spawnPos;

            var val = _randNumber.GetNumber();
            var boxVal = _current.GetComponent<IValue<int>>();
            boxVal.Value = val;

            var rig = _current.GetComponent<Rigidbody>();
            rig.isKinematic = true;
            rig.detectCollisions = true;
            enabled = true;
        }

        public void Ended()
        {
            enabled = false;
            var boxRigidbody = _current.GetComponent<Rigidbody>();
            boxRigidbody.isKinematic = false;
            _launcher.Launch(boxRigidbody);
        }
    }
}