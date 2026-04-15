using UnityEngine;
using TheGame.Common;

namespace TheGame
{
#if UNITY_EDITOR
    using UnityEditor;

    // to render wrappee directly, instead of with its container
    [CustomPropertyDrawer(typeof(Wrap<>), true)]
    public class WrapDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var wrappeeProp = property.FindPropertyRelative("_wrappee");
            EditorGUI.ObjectField(rect, wrappeeProp, label);
        }
    }
#endif
}