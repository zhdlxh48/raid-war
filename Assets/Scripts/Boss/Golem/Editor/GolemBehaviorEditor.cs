using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Boss.GolemBehavior))]
public class GolemBehaviorEditor : Editor
{
    SerializedProperty EditorStartLevel;
    SerializedProperty EditorCurHP;
    SerializedProperty EditorStartSTR;
    SerializedProperty EditorStartDEX;
    SerializedProperty EditorStartINT;
    SerializedProperty EditorMoveSpeed;

    SerializedProperty EditerCurrentPhase;
    SerializedProperty EditorExcuteSkillTime;
    SerializedProperty EditorIdleWaitTime;
    SerializedProperty EditorCastingRisesTime;
    SerializedProperty EditorAttackCastingRise;
    SerializedProperty EditorJointExceptionDistance;

    SerializedProperty EditorPlayerTransform;
    SerializedProperty EditorCamSakeEvent;
    SerializedProperty EditorPlayerOnDamage;
    SerializedProperty EditorPlayerOnDamageRig;
    SerializedProperty EditorBossMaterial;
    SerializedProperty EditorBossStartSound;

    bool mNomalFold;
    bool mPatternFold;
    bool mEtcFold;

    readonly GUIContent NomalSet = new GUIContent("기본 정보");
    readonly GUIContent PatternSet = new GUIContent("패턴 설정");
    readonly GUIContent EtcSet = new GUIContent("기타 설정");

    private void OnEnable()
    {
        EditorStartLevel = serializedObject.FindProperty("startLevel");
        EditorCurHP = serializedObject.FindProperty("curHP");
        EditorStartSTR = serializedObject.FindProperty("startSTR");
        EditorStartDEX = serializedObject.FindProperty("startDEX");
        EditorStartINT = serializedObject.FindProperty("startINT");
        EditorMoveSpeed = serializedObject.FindProperty("moveSpeed");

        EditerCurrentPhase = serializedObject.FindProperty("phase");
        EditorExcuteSkillTime = serializedObject.FindProperty("excuteSkillTime");
        EditorIdleWaitTime = serializedObject.FindProperty("idleWaitTime");
        EditorCastingRisesTime = serializedObject.FindProperty("castingRisesTime");
        EditorAttackCastingRise = serializedObject.FindProperty("attackCastingRises");
        EditorJointExceptionDistance = serializedObject.FindProperty("jointExceptDistance");

        EditorPlayerTransform = serializedObject.FindProperty("playerTransforms");
        EditorCamSakeEvent = serializedObject.FindProperty("camShake");
        EditorPlayerOnDamage = serializedObject.FindProperty("playerOnDamage");
        EditorPlayerOnDamageRig = serializedObject.FindProperty("playerOnDamageRig");
        EditorBossMaterial = serializedObject.FindProperty("bossBodyMat");
        EditorBossStartSound = serializedObject.FindProperty("soundEffect");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.LabelField(NomalSet);
        EditorGUILayout.LabelField(new GUIContent($"현제 페이즈 : {(target as Boss.GolemBehavior).phase}"));
        EditorGUILayout.PropertyField(EditorStartLevel, new GUIContent("레벨"));
        EditorGUILayout.PropertyField(EditorCurHP, new GUIContent("현재 체력"));
        EditorGUILayout.PropertyField(EditorStartSTR, new GUIContent("힘"));
        EditorGUILayout.PropertyField(EditorStartDEX, new GUIContent("민첩"));
        EditorGUILayout.PropertyField(EditorStartINT, new GUIContent("지능"));
        EditorGUILayout.PropertyField(EditorMoveSpeed, new GUIContent("속도"));

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        mPatternFold = EditorGUILayout.Foldout(mPatternFold, PatternSet);

        if (mPatternFold)
        {
            EditorGUILayout.PropertyField(EditorExcuteSkillTime, new GUIContent("스킬 시전 시간"));
            EditorGUILayout.PropertyField(EditorIdleWaitTime, new GUIContent("기본 상태 대기 시간"));
            EditorGUILayout.PropertyField(EditorCastingRisesTime, new GUIContent("캐스팅 시간"));
            EditorGUILayout.PropertyField(EditorAttackCastingRise, new GUIContent("공격당 스킬 게이지"));
            EditorGUILayout.PropertyField(EditorJointExceptionDistance, new GUIContent("스킬 조건 거리"));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;


        mEtcFold = EditorGUILayout.Foldout(mEtcFold, EtcSet);

        if (mEtcFold)
        {
            EditorGUILayout.PropertyField(EditorPlayerTransform, true);
            EditorGUILayout.PropertyField(EditorCamSakeEvent, new GUIContent("카메라 흔들림"));
            EditorGUILayout.PropertyField(EditorPlayerOnDamage, new GUIContent("플레이어 데미지 이벤트"));
            EditorGUILayout.PropertyField(EditorPlayerOnDamageRig, new GUIContent("플레이어 데미지(강) 이벤트"));
            EditorGUILayout.PropertyField(EditorBossMaterial, new GUIContent("보스 머티리얼"));
            EditorGUILayout.PropertyField(EditorBossStartSound, new GUIContent("보스 시작 효과음"));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
