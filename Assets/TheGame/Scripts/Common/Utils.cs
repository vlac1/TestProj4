using UnityEngine;

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
    }
}