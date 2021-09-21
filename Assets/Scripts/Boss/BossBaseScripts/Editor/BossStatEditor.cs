using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boss.BossStat))]
public class BossStatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Boss.BossStat valuField = (target as Boss.BossStat);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(new GUIContent("현재 능력치"));
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUILayout.LabelField(new GUIContent($"레벨: {valuField.LEVEL}"));
        EditorGUILayout.LabelField(new GUIContent($"힘: {valuField.STR}"));
        EditorGUILayout.LabelField(new GUIContent($"민첩: {valuField.DEX}"));
        EditorGUILayout.LabelField(new GUIContent($"지능: {valuField.INT}"));
        EditorGUILayout.LabelField(new GUIContent($"이동 속도: {valuField.MoveSpeed}"));

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorStyles.label.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(new GUIContent("현재 세부 능력치"));
        EditorStyles.label.fontStyle = FontStyle.Normal;

        EditorGUILayout.LabelField(new GUIContent($"체력: {valuField.HP}"));
        EditorGUILayout.LabelField(new GUIContent($"스킬 게이지: {valuField.MaxSkillGauge}"));
        EditorGUILayout.LabelField(new GUIContent($"초당 게이지 회복: {valuField.TPSGauge}"));
        EditorGUILayout.LabelField(new GUIContent($"공격시 게이지 회복: {valuField.ASGauge}"));
        EditorGUILayout.LabelField(new GUIContent($"최소 공격력: {valuField.MinDamage}"));
        EditorGUILayout.LabelField(new GUIContent($"최대 공격력: {valuField.MaxDamage}"));
        EditorGUILayout.LabelField(new GUIContent($"공격 속도: {valuField.AttackSpeed}"));
        EditorGUILayout.LabelField(new GUIContent($"방어력: {valuField.Armor}"));
        EditorGUILayout.LabelField(new GUIContent($"물리 근접 내성: {valuField.PNT}"));
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
