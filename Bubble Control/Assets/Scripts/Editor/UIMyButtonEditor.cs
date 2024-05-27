using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(UIMyButton))]
public class UIMyButtonEditor : UnityEditor.UI.ButtonEditor
{
    private SerializedProperty iconProperty;
    private SerializedProperty iconProperty2;

    protected override void OnEnable()
    {
        base.OnEnable();
        iconProperty = serializedObject.FindProperty("myText");
        iconProperty2 = serializedObject.FindProperty("myImage");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.ObjectField(iconProperty);
        EditorGUILayout.ObjectField(iconProperty2);
        serializedObject.ApplyModifiedProperties();
    }
}
