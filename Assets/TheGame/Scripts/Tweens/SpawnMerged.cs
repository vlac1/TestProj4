using System.Linq;//evil
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Tweens
{
    internal class SpawnMerged : MonoBehaviour, ITween
    {
        [SerializeField] private float _planeHeight = 3f;
        [SerializeField] private ParticleSystem _particles;

        [Inject]
        private IStorage<GameObject> _boxStorage;//src

        public UniTask Execute<T>(IArrayish<T> group) where T: Component
        {
            var groupCenter = Utils.GroupCenter(group);
            groupCenter.y = _planeHeight;
            var sumOfAll = 0;// group.Sum(E => E.GetComponent<IValue<int>>().Value);//small but slow
            for (var i = 0; i < group.Count; i++)
            {
                var entity = group[i];
                var score = entity.GetComponent<IValue<int>>().Value;
                sumOfAll += score;
            }

            Spawn_Merged(groupCenter, sumOfAll);
            return UniTask.CompletedTask;
        }

        private void Spawn_Merged(Vector3 center, int sumOfAll)
        {
            _particles.transform.position = center;
            _particles.Play();

            var mergedAllBox = _boxStorage.Request();
            var mergedAllBoxVal = mergedAllBox.GetComponent<IValue<int>>();
            mergedAllBoxVal.Value = sumOfAll;
            mergedAllBox.transform.position = center;
        }
    }
}