using UnityEngine;
using UnityEngine.EventSystems;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Inputs
{
    internal class WindowsInput : MonoBehaviour
    {
        [SerializeField] private bool _ignoreUI;
        [SerializeField] private Wrap<IState> _state;

        private bool IsPointerOverUI
            => EventSystem.current.IsPointerOverGameObject();

#if UNITY_EDITOR && UNITY_ANDROID
        private void Update()
        {
            Process();
        }
#endif

        private void Process()
        {
            if (_ignoreUI && IsPointerOverUI) return;

            if (Input.GetMouseButtonDown(0))
                _state.Wrappee.Began();

            //if (Input.GetMouseButton(0))
            //    _onHold.Invoke();

            if (Input.GetMouseButtonUp(0))
                _state.Wrappee.Ended();
        }
    }
}