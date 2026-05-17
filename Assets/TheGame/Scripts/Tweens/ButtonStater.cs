using UnityEngine;
using UnityEngine.UI;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using TheGame.Common;

namespace TheGame
{
    internal class ButtonStater : MonoBehaviour, IGroupProc
    {
        [SerializeField] private bool _stateBefore;
        [SerializeField] private Button _mergeButton;
        [SerializeField] private Wrap<IGroupProc> _tween;

        public async UniTask Process<T>(IGroup<T> group) where T : Component
        {
            _mergeButton.interactable = _stateBefore;//No callbacks! huh
            await _tween.Wrappee.Process(group);
            _mergeButton.interactable = !_stateBefore;
        }
    }
}