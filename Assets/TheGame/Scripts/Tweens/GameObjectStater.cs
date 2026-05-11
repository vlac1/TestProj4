using UnityEngine;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;
using TheGame.Common;

namespace TheGame
{
    internal class GameObjectStater : MonoBehaviour, ITween
    {
        [SerializeField] private bool _stateBefore;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Wrap<ITween> _tween;

        public async UniTask Execute<T>(IArrayish<T> group) where T : Component
        {
            _gameObject.SetActive(_stateBefore);
            await _tween.Wrappee.Execute(group);
            _gameObject.SetActive(!_stateBefore);
        }
    }
}