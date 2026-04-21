using Cysharp.Threading.Tasks;
using UnityEngine;
using TheGame.Common;

namespace TheGame.Tweens
{
    public class SwingBackTween : BaseTween
    {
        [SerializeField] private float _swingBackDist = .5f;//offset from origin

        private Vector3 _groupCenter;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
        {
            var offset = (currentPos - _groupCenter).normalized * _swingBackDist;
            return currentPos + offset;
        }

        public override UniTask Execute<T>(T[] objects)
        {
            _groupCenter = Utils.GroupCenter(objects);
            return base.Execute(objects);
        }
    }
}