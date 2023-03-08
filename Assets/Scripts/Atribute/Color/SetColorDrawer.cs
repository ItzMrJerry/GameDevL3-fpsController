#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomPropertyDrawer(typeof(SetColor))]
public class SetColorDrawer : PropertyDrawer
{
#if UNITY_EDITOR
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SetColor setcolor = attribute as SetColor;
        var defaultColor = GUI.color;
        GUI.color = new Color(setcolor.r/255, setcolor.g/255, setcolor.b/255);
        EditorGUI.PropertyField(position, property, label);
        GUI.color = defaultColor;

    }
#endif
}
