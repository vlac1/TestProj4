using UnityEngine;
using TheGame.Interfaces;
using TheGame.Common;

namespace TheGame
{
    internal class GroupProcessor : MonoBehaviour
    {
        [SerializeField] private Wrap<IArrayish<BoxEntity>> _group;
        [SerializeField] private Wrap<ITween> _tween;

        public async void ProcessGroup()//from UI
        {
            // tweens: FlyUp, SwingBack, FlyToCenter, SpawnMerged
            await _tween.Wrappee.Execute(_group.Wrappee);// boxes
        }
    }
}