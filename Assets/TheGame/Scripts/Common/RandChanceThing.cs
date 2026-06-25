using System;
using System.Linq;//evil
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheGame.Common
{
    public class RandChanceThing<T> : MonoBehaviour
    {
        [SerializeField] private ChanceInfo[] _chances;

        //private int[] _indices;

        // TODO auto even Percent spread
        private void OnValidate()
        {
            var sumPerc = _chances.Sum(chance => chance.Percent);
            if (sumPerc != 100)
                Debug.LogError("Percent sum Have to be 100");
        }

        private void FillIndices(Span<int> indices)
        {
            var offset = 0;
            for (int i = 0; i < _chances.Length; i++)
            {
                var chance = _chances[i];
                indices.Slice(offset, chance.Percent).Fill(i);
                offset += chance.Percent;
            }
        }

        public T GetThing()
        {
            Span<int> indices = stackalloc int[100];
            FillIndices(indices);
            //if(_indices == null)//or Awake
            //    FillIndices(_indices = new int[100]);

            var randIndex = Random.Range(0, 100);
            var index = indices[randIndex];
            return _chances[index].Thing;
        }

        [Serializable]
        private class ChanceInfo
        {
            [field: SerializeField] public T Thing { get; private set; }
            [field: SerializeField, Range(0, 100)] public int Percent { get; private set; }
        }
    }
}