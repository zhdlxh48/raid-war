using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowCamera))]
public class FollowCameraEditor : Editor
{
    SerializedProperty EditorTarget;
    SerializedProperty EditorYFollowDist;
    SerializedProperty EditorYFollowHeight;
    SerializedProperty EditorFollowSpeed;

    SerializedProperty EditorShakeRadius;
    SerializedProperty EditorShakeTime;

    bool mFollowFold;
    bool mShakeFold;
    bool mShaderFold;

    readonly GUIContent mFollowSet = new GUIContent("팔로우 설정");
    readonly GUIContent mShakeSet = new GUIContent("효과 설정");
    readonly GUIContent mShaderSet = new GUIContent("쉐이더");

    private void OnEnable()
    {
        EditorTarget = serializedObject.FindProperty("followTarget");
        EditorYFollowDist = serializedObject.FindProperty("zFollowDist");
        EditorYFollowHeight = serializedObject.FindProperty("yFollowHeight");
        EditorFollowSpeed = serializedObject.FindProperty("followSpeed");

        EditorShakeRadius = serializedObject.FindProperty("ShakeRadius");
        EditorShakeTime = serializedObject.FindProperty("ShakeTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        mFollowFold = EditorGUILayout.Foldout(mFollowFold, mFollowSet);

        if (mFollowFold)
        {
            EditorGUILayout.PropertyField(EditorTarget, new GUIContent("타겟"));
            EditorGUILayout.PropertyField(EditorYFollowDist, new GUIContent("거리"));
            EditorGUILayout.PropertyField(EditorYFollowHeight, new GUIContent("높이"));
            EditorGUILayout.PropertyField(EditorFollowSpeed, new GUIContent("속도"));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        mShakeFold = EditorGUILayout.Foldout(mShakeFold, mShakeSet);

        if (mShakeFold)
        {
            EditorGUILayout.PropertyField(EditorShakeRadius, new GUIContent("크기"));
            EditorGUILayout.PropertyField(EditorShakeTime, new GUIContent("시간"));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
