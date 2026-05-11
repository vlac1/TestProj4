using UnityEngine;
using UnityEngine.UI;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using TheGame.Common;

namespace TheGame
{
    internal class ButtonStater : MonoBehaviour, ITween
    {
        [SerializeField] private bool _stateBefore;
        [SerializeField] private Button _mergeButton;
        [SerializeField] private Wrap<ITween> _tween;

        public async UniTask Execute<T>(IArrayish<T> group) where T : Component
        {
            _mergeButton.interactable = _stateBefore;//No callbacks! huh
            await _tween.Wrappee.Execute(group);
            _mergeButton.interactable = !_stateBefore;
        }
    }
}