using UnityEngine;

namespace TheGame.Tweens
{
    public class FlyUpTween : BaseTween
    {
        [SerializeField] private float _flyUpHeight = 5f;

        protected override Vector3 ComputeTarget(Vector3 currentPos)
            => currentPos + Vector3.up * _flyUpHeight;
    }
}