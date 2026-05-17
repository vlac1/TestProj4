using UnityEngine;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame.Tweens
{
    internal class Tweens : MonoBehaviour, IGroupProc// arr dec
    {
        [SerializeField] private Wrap<IGroupProc>[] _tweens;

        public async UniTask Process<T>(IGroup<T> group) where T : Component
        {
            for (var i = 0; i < _tweens.Length; i++)
            {
                await _tweens[i].Wrappee.Process(group);
            }
        }
    }
}