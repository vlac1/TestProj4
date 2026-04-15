using System;
using System.Linq;//evil
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheGame.Common
{
    public class RandNumber : MonoBehaviour
    {
        [SerializeField] private ChanceInfo[] _chances;

        // TODO auto even Percent spread
        private void OnValidate()
        {
            var sumPerc = _chances.Sum(chance => chance.Percent);
            if (sumPerc != 100)
                Debug.LogError("Percent sum Have to be 100");
        }

        public int GetNumber()
        {
            Span<int> numbers = stackalloc int[100];// or new int[100] on Awake

            var offset = 0;
            foreach (var chance in _chances)
            {
                var block = numbers.Slice(offset, chance.Percent);
                block.Fill(chance.Number);
                offset += chance.Percent;
            }
            var randIndex = Random.Range(0, 100);
            return numbers[randIndex];
        }

        [Serializable]
        private class ChanceInfo
        {
            [field: SerializeField] public int Number { get; private set; }
            [field: SerializeField, Range(0, 100)] public int Percent { get; private set; }
        }
    }
}