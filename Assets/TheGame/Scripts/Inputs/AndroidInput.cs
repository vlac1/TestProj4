using UnityEngine;
using UnityEngine.EventSystems;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Inputs
{
    internal class AndroidInput : MonoBehaviour
    {
        [SerializeField] private bool _ignoreUI;
        [SerializeField] private Wrap<IState> _state;

        private bool IsPointerOverUI
            => EventSystem.current.IsPointerOverGameObject(0);

#if !UNITY_EDITOR && UNITY_ANDROID
        private void Update()
        {
            Process();
        }
#endif

        private void Process()
        {
            if (_ignoreUI && IsPointerOverUI) return;

            if (Input.touchCount != 1) return;

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _state.Wrappee.Began();
                    break;
                //case TouchPhase.Stationary: break;
                case TouchPhase.Ended:
                    _state.Wrappee.Ended();
                    break;
            }
        }
    }
}