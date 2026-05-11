using UnityEngine;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Tweens
{
    internal class Tweens : MonoBehaviour, ITween// arr dec
    {
        [SerializeField] private Wrap<ITween>[] _tweens;

        public async UniTask Execute<T>(IArrayish<T> group) where T : Component
        {
            for (var i = 0; i < _tweens.Length; i++)
            {
                await _tweens[i].Wrappee.Execute(group);
            }
        }
    }
}