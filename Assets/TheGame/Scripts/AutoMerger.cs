using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class AutoMerger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private Wrap<IAnimator> _animator;
        [SerializeField] private Button _mergeButton;
        [SerializeField] private UnityEvent _beforeMerge;
        [SerializeField] private UnityEvent<int> _afterMerge;

        [Inject]
        private IStorage<GameObject> _boxStorage;//src

        public async void Merge()//from UI
        {
            _beforeMerge.Invoke();
            _mergeButton.interactable = false;//No callbacks! huh
            var score = await MergeAll();
            _mergeButton.interactable = true;
            _afterMerge.Invoke(score);
        }

        private void ReturnAll<T>(T[] boxes) where T : Component
        {
            foreach (var box in boxes)
            {
                _boxStorage.Return(box.gameObject);
            }
        }

        private void SpawnMerged(Vector3 center, int sumOfAll)
        {
            _particles.transform.position = center;
            _particles.Play();
            var mergedAllBox = _boxStorage.Request();
            var mergedAllBoxVal = mergedAllBox.GetComponent<IValue<int>>();
            mergedAllBoxVal.Value = sumOfAll;
            mergedAllBox.transform.position = center;
        }

        private void GetGroupInfo(Merger[] boxes, out int sumOfAll, out Vector3 center)
        {
            sumOfAll = 0;
            center = Vector3.zero;
            foreach (var box in boxes)
            {
                var boxT = box.transform;
                var boxPos = boxT.position;
                var upPos = boxPos + Vector3.up * _flyUpHeight;

                sumOfAll += box.Score.Value;
                center += upPos;
            }
            center /= boxes.Length;//aver
        }

        private async UniTask<int> MergeAll()
        {
            // TODO or use pool active list
            var boxes = FindObjectsByType<Merger>(FindObjectsSortMode.None);
            GetGroupInfo(boxes, out var sumOfAll, out var center);

            foreach(var box in boxes)
            {
                var boxRig = box.GetComponent<Rigidbody>();
                boxRig.isKinematic = true;
                boxRig.detectCollisions = false;
            }

            // TODO tweens: FlyUp, SwingBack, FlyToCenter
            //foreach(var tween in tweens)
            //    await tween.Exec(boxes);
            await FlyUp(boxes);
            await SwingBack(boxes, center);
            await FlyToCenter(boxes, center);

            ReturnAll(boxes);
            await UniTask.Yield();//finish ReturnAll

            SpawnMerged(center, sumOfAll);
            return sumOfAll;
        }

        #region Fly Up

        [SerializeField] private int _flyUpMs = 1000;
        [SerializeField] private float _flyUpHeight = 5f;

        private UniTask FlyUp<T>(T[] boxes) where T : Component
            => _animator.Wrappee.AnimateGroup(boxes, _flyUpMs,
                boxPos => boxPos + Vector3.up * _flyUpHeight, Vector3.Lerp);

        #endregion

        #region Swing Back

        [SerializeField] private int _swingBackMs = 500;
        [SerializeField] private float _swingBackDist = .5f;
        [SerializeField] private AnimationCurve _swingBackFunc;

        private Vector3 SwingBackFunc(Vector3 a, Vector3 b, float t)
        {
            t = _swingBackFunc.Evaluate(t);//non linear
            return Vector3.Lerp(a, b, t);
        }

        private UniTask SwingBack<T>(T[] boxes, Vector3 center) where T : Component
            => _animator.Wrappee.AnimateGroup(boxes, _swingBackMs, boxPos =>
            {
                var offset = (boxPos - center).normalized * _swingBackDist;
                return boxPos + offset;
            }, SwingBackFunc);

        #endregion

        #region Fly To Center

        [SerializeField] private int _toCenterMs = 500;
        [SerializeField] private AnimationCurve _toCenterFunc;

        private Vector3 ToCenterFunc(Vector3 a, Vector3 b, float t)
        {
            t = _toCenterFunc.Evaluate(t);//non linear
            return Vector3.Lerp(a, b, t);
        }

        private UniTask FlyToCenter<T>(T[] boxes, Vector3 center) where T : Component
            => _animator.Wrappee.AnimateGroup(boxes, _toCenterMs, boxPos => center, ToCenterFunc);

        #endregion
    }
}