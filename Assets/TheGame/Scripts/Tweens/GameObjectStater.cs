using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using TheGame.Common;

namespace TheGame
{
    internal class GameObjectStater : MonoBehaviour, IGroupProc
    {
        [SerializeField] private bool _stateBefore;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Wrap<IGroupProc> _tween;

        public async UniTask Process<T>(IGroup<T> group) where T : Component
        {
            _gameObject.SetActive(_stateBefore);
            await _tween.Wrappee.Process(group);
            _gameObject.SetActive(!_stateBefore);
        }
    }
}