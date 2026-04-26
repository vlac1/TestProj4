using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    internal interface ITween//AsyncGroupProcessor
    {
        //IEnumerable<T> items
        UniTask Execute<T>(T[] items) where T : Component;
    }
}