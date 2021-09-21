using UnityEngine;
using System.Collections;

public class InputControlUtil
{
    public static float InputLeftRightValue()
    {
        if (!GameKey.GetKey(GameKeyPreset.LeftArrow) && GameKey.GetKey(GameKeyPreset.RightArrow))
        {
            return 1.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.LeftArrow) && !GameKey.GetKey(GameKeyPreset.RightArrow))
        {
            return -1.0f;
        }

        return 0.0f;
    }
    public static float InputUpDownValue()
    {
        if (!GameKey.GetKey(GameKeyPreset.DownArrow) && GameKey.GetKey(GameKeyPreset.UpArrow))
        {
            return 1.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.DownArrow) && !GameKey.GetKey(GameKeyPreset.UpArrow))
        {
            return -1.0f;
        }

        return 0.0f;
    }
}
