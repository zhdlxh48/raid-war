using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseState { MOVE, ATTACK, ITEM }

public class MouseCursor : MonoBehaviour
{
    public Texture2D mouseCursorTexture;
    public Texture2D[] mouseCursorTextures;

    public MouseState startCursorState;
    public MouseState currentCursorState;

    private Dictionary<MouseState, Texture2D> mouseCursors = new Dictionary<MouseState, Texture2D>();

    public bool fixPointIsCenter;
    public Vector2 fixPointAdjust;

    private Vector2 hotspot;

    private void Awake()
    {
        InitMousePointer();
        SetMouseCursor(startCursorState);
    }

    private void InitMousePointer()
    {
        mouseCursors.Add(MouseState.MOVE, mouseCursorTextures[(int)MouseState.MOVE]);
        mouseCursors.Add(MouseState.ATTACK, mouseCursorTextures[(int)MouseState.ATTACK]);
        mouseCursors.Add(MouseState.ITEM, mouseCursorTextures[(int)MouseState.ITEM]);
    }

    public void SetMouseCursor(MouseState stat)
    {
        currentCursorState = stat;
        mouseCursorTexture = mouseCursors[stat];

        if (mouseCursorTexture != null)
            StartCoroutine(SetCursorTexture(mouseCursorTexture));
    }

    private IEnumerator SetCursorTexture(Texture2D setCur)
    {
        yield return new WaitForEndOfFrame();

        if (fixPointIsCenter)
        {
            hotspot.x = mouseCursorTexture.width / 2;
            hotspot.y = mouseCursorTexture.height / 2;
        }
        else
        {
            hotspot = fixPointAdjust;
        }

        Cursor.SetCursor(mouseCursorTexture, hotspot, CursorMode.Auto);

        yield break;
    }
}
