using UnityEngine;
using UnityEngine.Events;
using TheGame.Common;
using TheGame.Interfaces;
using Cysharp.Threading.Tasks;

namespace TheGame
{
    // TODO limited auto merge attempts
    internal class GameOver : MonoBehaviour, IGroupProc
    {
        [SerializeField] private int _minScore;
        [SerializeField] private int _maxScore;
        [SerializeField] private Wrap<ISetText> _goalSet;//or UnityEvent<string>
        [SerializeField] private UnityEvent _win;
        [SerializeField] private UnityEvent _lose;

        private int _goalScore;

        private void Awake()
        {
            // make even number
            _goalScore = Random.Range(_minScore, _maxScore) & -2;
            _goalSet.Wrappee.SetText(_goalScore.ToString());
        }

        public void EvalScore(int score)
        {
            if (score == _goalScore)
                _win.Invoke();
            if (score > _goalScore)
                _lose.Invoke();
        }

        public UniTask Process<T>(IGroup<T> group) where T : Component
        {
            var boxes = (IGroup<BoxEntity>)group;

            var sumOfAll = 0;// boxes.Sum(E => E.Score.Value);//small but slow
            for (var i = 0; i < boxes.Count; i++)
                sumOfAll += boxes[i].Score.Value;

            EvalScore(sumOfAll);
            return UniTask.CompletedTask;
        }
    }
}