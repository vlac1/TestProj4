using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;

namespace TheGame.Common
{
    using LerpFunc = Func<Vector3, Vector3, float, Vector3>;

    internal class SimpleAnimator : MonoBehaviour, IAnimator
    {
        readonly List<UniTask> running = new(200);

        public async UniTask AnimateGroup<T>(T[] items, int ms, Func<Vector3, Vector3> computeTarget, LerpFunc func)
            where T : Component
        {
            running.Clear();
            //UniTask[] running = new UniTask[boxes.Length];//alloc
            foreach(var item in items)
            //for (int i = 0; i < items.Length; i++)
            {
                //var item = items[i];
                var itemT = item.transform;
                var boxPos = itemT.position;
                var targetPos = computeTarget(boxPos);
                var secs = ms / 1000f;
                var task = AnimateToTargetAsync(itemT, targetPos, secs, func);
                //task.Forget();//BUG cant rely on same time
                running.Add(task);
            }
            //await UniTask.Delay(ms);
            await UniTask.WhenAll(running);
            await UniTask.Yield();
        }

        public static async UniTask AnimateToTargetAsync(Transform item, Vector3 targetPos, float duration, Func<Vector3, Vector3, float, Vector3> func)
        {
            var startPos = item.position;

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                var pos = func(startPos, targetPos, t);
                item.position = pos;
                await UniTask.Yield();
            }
            item.position = targetPos;
        }
    }
}