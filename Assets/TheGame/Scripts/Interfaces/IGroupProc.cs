using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    /// <summary>
    /// Async Group Processor
    /// </summary>
    internal interface IGroupProc//old ITween//ren AsyncGroupProcessor
    {
        UniTask Process<T>(IGroup<T> group) where T : Component;
    }
}