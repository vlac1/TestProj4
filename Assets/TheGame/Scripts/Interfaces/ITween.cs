using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    internal interface ITween//ren AsyncGroupProcessor
    {
        UniTask Execute<T>(IArrayish<T> group) where T : Component;
    }
}