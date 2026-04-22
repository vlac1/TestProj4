using Cysharp.Threading.Tasks;
using UnityEngine;
using TheGame.Common;

namespace TheGame.Tweens
{
    public class SwingBackTween : BaseTween
    {
        [SerializeField] private float _swingBackDist = .5f;//offset from origin
        [SerializeField] private float _planeHeight = 3f;

        private Vector3 _groupCenter;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
        {
            var offset = (currentPos - _groupCenter).normalized * _swingBackDist;
            return currentPos + offset;
        }

        public override UniTask Execute<T>(T[] objects)
        {
            _groupCenter = Utils.GroupCenter(objects);
            // on plane above (if box Falls out of map, center is below map (or clear box))
            _groupCenter.y = _planeHeight;
            return base.Execute(objects);
        }
    }
}