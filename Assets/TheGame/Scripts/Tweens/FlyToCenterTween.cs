using Cysharp.Threading.Tasks;
using UnityEngine;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Tweens
{
    internal class FlyToCenterTween : BaseTween
    {
        [SerializeField] private float _planeHeight = 3f;
        
        private Vector3 _groupCenter;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
            => _groupCenter;

        public override UniTask Execute<T>(IArrayish<T> group)
        {
            _groupCenter = Utils.GroupCenter(group);
            _groupCenter.y = _planeHeight;
            return base.Execute(group);
        }
    }
}