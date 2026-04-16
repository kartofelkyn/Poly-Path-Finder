#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GradientGraphic)), CanEditMultipleObjects]
public class GradientGraphicEditor : Editor
{
    SerializedProperty spriteProp;
    SerializedProperty topLeftColorProp;
    SerializedProperty topRightColorProp;
    SerializedProperty bottomLeftColorProp;
    SerializedProperty bottomRightColorProp;
    SerializedProperty gradientSmoothnessProp;

    void OnEnable()
    {
        spriteProp = serializedObject.FindProperty("sprite");
        topLeftColorProp = serializedObject.FindProperty("topLeftColor");
        topRightColorProp = serializedObject.FindProperty("topRightColor");
        bottomLeftColorProp = serializedObject.FindProperty("bottomLeftColor");
        bottomRightColorProp = serializedObject.FindProperty("bottomRightColor");
        gradientSmoothnessProp = serializedObject.FindProperty("gradientSmoothness");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(spriteProp);
        EditorGUILayout.PropertyField(topLeftColorProp);
        EditorGUILayout.PropertyField(topRightColorProp);
        EditorGUILayout.PropertyField(bottomLeftColorProp);
        EditorGUILayout.PropertyField(bottomRightColorProp);
        EditorGUILayout.PropertyField(gradientSmoothnessProp);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif