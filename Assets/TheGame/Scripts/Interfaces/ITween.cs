using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    internal interface ITween
    {
        UniTask Execute<T>(T[] items) where T : Component;
    }
}