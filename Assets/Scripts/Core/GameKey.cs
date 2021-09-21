using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameKeyPreset
{
    NONE,
    LeftArrow, RightArrow, UpArrow, DownArrow, Dash,
    NormalAttack, Skill_1, Skill_2, Skill_3, Skill_Ultimate,
    ITEM_1, ITEM_2, ITEM_3, ITEM_4, ITEM_5, ITEM_6
}

public class KeyData
{
    public KeyCode initKey;
    public KeyCode customKey;

    public KeyData(KeyCode key)
    {
        initKey = key;
        customKey = key;
    }
}

public class GameKey : MonoBehaviour
{
    public static Dictionary<GameKeyPreset, KeyData> GameKeys;

    public static GameKeyPreset[] moveKeys;
    public static GameKeyPreset[] skillKeys;
    public static GameKeyPreset[] itemUseKeys;

    private void Awake()
    {
        GameKeys = new Dictionary<GameKeyPreset, KeyData>();

        moveKeys = new GameKeyPreset[4];
        skillKeys = new GameKeyPreset[1];
        itemUseKeys = new GameKeyPreset[6];

        InitKeyMapping();
        InitPlayerDefaultKey();
    }

    private void InitKeyMapping()
    {
        GameKeys[GameKeyPreset.NONE] = new KeyData(KeyCode.None);

        GameKeys[GameKeyPreset.LeftArrow] = new KeyData(KeyCode.LeftArrow);
        GameKeys[GameKeyPreset.RightArrow] = new KeyData(KeyCode.RightArrow);
        GameKeys[GameKeyPreset.DownArrow] = new KeyData(KeyCode.DownArrow);
        GameKeys[GameKeyPreset.UpArrow] = new KeyData(KeyCode.UpArrow);

        GameKeys[GameKeyPreset.Dash] = new KeyData(KeyCode.Space);

        GameKeys[GameKeyPreset.NormalAttack] = new KeyData(KeyCode.A);

        GameKeys[GameKeyPreset.Skill_1] = new KeyData(KeyCode.Q);
        GameKeys[GameKeyPreset.Skill_2] = new KeyData(KeyCode.W);
        GameKeys[GameKeyPreset.Skill_3] = new KeyData(KeyCode.E);
        GameKeys[GameKeyPreset.Skill_Ultimate] = new KeyData(KeyCode.R);

        GameKeys[GameKeyPreset.ITEM_1] = new KeyData(KeyCode.Alpha1);
        GameKeys[GameKeyPreset.ITEM_2] = new KeyData(KeyCode.Alpha2);
        GameKeys[GameKeyPreset.ITEM_3] = new KeyData(KeyCode.Alpha3);
        GameKeys[GameKeyPreset.ITEM_4] = new KeyData(KeyCode.Alpha4);
        GameKeys[GameKeyPreset.ITEM_5] = new KeyData(KeyCode.Alpha5);
        GameKeys[GameKeyPreset.ITEM_6] = new KeyData(KeyCode.Alpha6);
    }
    private void InitPlayerDefaultKey()
    {
        moveKeys[0] = GameKeyPreset.LeftArrow;
        moveKeys[1] = GameKeyPreset.RightArrow;
        moveKeys[2] = GameKeyPreset.DownArrow;
        moveKeys[3] = GameKeyPreset.UpArrow;

        skillKeys[0] = GameKeyPreset.Skill_1;

        itemUseKeys[0] = GameKeyPreset.ITEM_1;
        itemUseKeys[1] = GameKeyPreset.ITEM_2;
        itemUseKeys[2] = GameKeyPreset.ITEM_3;
        itemUseKeys[3] = GameKeyPreset.ITEM_4;
        itemUseKeys[4] = GameKeyPreset.ITEM_5;
        itemUseKeys[5] = GameKeyPreset.ITEM_6;
    }

    public void ChangeCustomKey(GameKeyPreset targetKey, KeyCode changeKey)
    {
        GameKeys[targetKey].customKey = changeKey;
    }
    public void ResetToInitKey(GameKeyPreset targetKey)
    {
        GameKeys[targetKey].customKey = GameKeys[targetKey].initKey;
    }

    public static bool GetKeyDown(GameKeyPreset key)
    {
        return Input.GetKeyDown(GameKeys[key].customKey);
    }
    public static bool GetKeyUp(GameKeyPreset key)
    {
        return Input.GetKeyUp(GameKeys[key].customKey);
    }
    public static bool GetKey(GameKeyPreset key)
    {
        return Input.GetKey(GameKeys[key].customKey);
    }

    public static bool GetKeys(GameKeyPreset[] inputKeys)
    {
        foreach (var item in inputKeys)
            if (GetKey(item)) return true;

        return false;
    }
    public static bool GetKeysDown(GameKeyPreset[] inputKeys)
    {
        foreach (var item in inputKeys)
            if (GetKeyDown(item)) return true;

        return false;
    }
    public static bool GetKeysUp(GameKeyPreset[] inputKeys)
    {
        foreach (var item in inputKeys)
            if (GetKeyUp(item)) return true;

        return false;
    }

    public static GameKeyPreset ReturnInputKey()
    {
        foreach (var item in GameKeys.Keys)
            if (GetKey(item)) return item;

        return GameKeyPreset.NONE;
    }
    public static GameKeyPreset ReturnInputKey(GameKeyPreset[] inputKeys)
    {
        foreach (var item in inputKeys)
            if (GetKey(item)) return item;

        return GameKeyPreset.NONE;
    }
}