using Cysharp.Threading.Tasks;
using UnityEngine;
using TheGame.Common;

namespace TheGame.Tweens
{
    public class FlyToCenterTween : BaseTween
    {
        [SerializeField] private float _planeHeight = 3f;
        
        private Vector3 _groupCenter;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
            => _groupCenter;

        public override UniTask Execute<T>(T[] objects)
        {
            _groupCenter = Utils.GroupCenter(objects);
            _groupCenter.y = _planeHeight;
            return base.Execute(objects);
        }
    }
}