using UnityEngine;
using System.Collections;

public class MathUtil
{
    public static Vector3 AngleToDirectionVector(float angle, float currentEulerAngleY)
    {
        angle += currentEulerAngleY;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public static float DirectionVectorToAngle(Vector3 startVec, Vector3 destVec, Vector3 forwardVec)
    {
        return Vector3.Angle((destVec - startVec).normalized, forwardVec);
    }
}
