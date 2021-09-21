using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameMangerEditor : Editor
{
    GUILayoutOption[] option;
    GUILayoutOption[] layoption;

    SerializedProperty EditorMiniute;
    SerializedProperty EditorSecond;
    SerializedProperty EditorSubCam;
    SerializedProperty EditorSubBGM;

    bool mRefFold;

    readonly GUIContent SetRef = new GUIContent("제한 시간");

    private void OnEnable()
    {
        option = new GUILayoutOption[]
            {
                GUILayout.MaxWidth(30)
            };

        layoption = new GUILayoutOption[]
            {
                GUILayout.MaxWidth(80)
            };

        EditorMiniute = serializedObject.FindProperty("miniute");
        EditorSecond = serializedObject.FindProperty("second");
        EditorSubCam = serializedObject.FindProperty("subCam");
        EditorSubBGM = serializedObject.FindProperty("bgm");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.LabelField(SetRef);
        EditorGUILayout.BeginHorizontal();
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(EditorMiniute, new GUIContent(""), layoption);
        EditorGUILayout.LabelField(new GUIContent(":"), option);
        EditorGUILayout.PropertyField(EditorSecond, new GUIContent(""), layoption);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(EditorSubCam, new GUIContent("서브 카메라"));
        EditorGUILayout.PropertyField(EditorSubBGM, new GUIContent("배경 음악"));

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
