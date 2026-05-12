using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    internal interface ITween//ren AsyncGroupProcessor
    {
        UniTask Process<T>(IGroup<T> group) where T : Component;
    }
}