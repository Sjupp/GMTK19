using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIElement))]
[CanEditMultipleObjects]
public class UIElementEditor : Editor
{
    SerializedProperty interfaceState;

    private void OnEnable() {
        interfaceState = serializedObject.FindProperty("interfaceStates");
    }

    public override void OnInspectorGUI() {


        serializedObject.Update();


        EditorGUILayout.PropertyField(interfaceState, true);

        serializedObject.ApplyModifiedProperties();

    }
}
