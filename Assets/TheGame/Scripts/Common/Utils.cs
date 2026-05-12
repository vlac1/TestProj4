using UnityEngine;
using TheGame.Interfaces;

namespace TheGame.Common
{
    public static class Utils
    {
        public static Vector3 GroupCenter<T>(T[] boxes)
            where T : Component
        {
            var center = Vector3.zero;
            foreach (var box in boxes)
            {
                center += box.transform.position;
            }
            return center / boxes.Length;//aver
        }

        internal static Vector3 GroupCenter<T>(IGroup<T> group)
            where T : Component
        {
            var center = Vector3.zero;
            for (var i = 0; i < group.Count; i++)
            {
                var box = group[i];
                center += box.transform.position;
            }
            return center / group.Count;//aver
        }

    }
}