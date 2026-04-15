using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace TheGame.Interfaces
{
    using LerpFunc = Func<Vector3, Vector3, float, Vector3>;

    internal interface IAnimator
    {
        UniTask AnimateGroup<T>(T[] items, int ms, Func<Vector3, Vector3> computeTarget, LerpFunc func)
            where T : Component;
    }
}