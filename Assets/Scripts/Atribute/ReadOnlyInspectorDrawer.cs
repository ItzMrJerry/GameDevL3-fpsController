#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyInspector))]
public class ReadOnlyInspectorDrawer : PropertyDrawer
{
#if UNITY_EDITOR
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
#endif
}
