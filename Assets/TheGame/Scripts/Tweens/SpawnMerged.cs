using System.Linq;//evil
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using TheGame.Common;
using TheGame.Interfaces;

namespace TheGame.Tweens
{
    public class SpawnMerged : MonoBehaviour, ITween
    {
        [SerializeField] private float _planeHeight = 3f;
        [SerializeField] private ParticleSystem _particles;

        [Inject]
        private IStorage<GameObject> _boxStorage;//src

        public UniTask Execute<T>(T[] objects) where T: Component
        {
            var groupCenter = Utils.GroupCenter(objects);
            groupCenter.y = _planeHeight;
            var sumOfAll = objects.Sum(E => E.GetComponent<IValue<int>>().Value);//small but slow

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