using Cysharp.Threading.Tasks;
using UnityEngine;
using TheGame.Common;

namespace TheGame.Tweens
{
    public class FlyToCenterTween : BaseTween
    {
        private Vector3 _groupCenter;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
            => _groupCenter;

        public override UniTask Execute<T>(T[] objects)
        {
            _groupCenter = Utils.GroupCenter(objects);
            return base.Execute(objects);
        }
    }
}