using System.Linq;//evil
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class AutoMerger : MonoBehaviour//TweensPlayer
    {
        [SerializeField] private Wrap<ITween> _tween;
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

        private async UniTask<int> MergeAll()
        {
            // TODO or use pool active list
            var boxes = FindObjectsByType<BoxEntity>(FindObjectsSortMode.None);

            foreach (var box in boxes)
            {
                box.Rigidbody.isKinematic = true;
                box.Rigidbody.detectCollisions = false;
            }

            // tweens: FlyUp, SwingBack, FlyToCenter
            await _tween.Wrappee.Execute(boxes);

            var sumOfAll = boxes.Sum(E => E.Score.Value);//slow but small
            var center = Utils.GroupCenter(boxes);
            ReturnAll(boxes);
            SpawnMerged(center, sumOfAll);
            return sumOfAll;
        }
    }
}