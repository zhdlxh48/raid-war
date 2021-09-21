using UnityEngine;
using System.Collections;

public class MovementUtil
{
    public static void VectorMove(Rigidbody rigid, Vector3 direc, float moveSpeed)
    {
        rigid.position += direc * moveSpeed * Time.deltaTime;
    }

    public static void PointMove(Rigidbody rigid, Vector3 curPos, Vector3 destPos, float moveSpeed)
    {
        destPos.y = curPos.y;

        Vector3 deltaMove = Vector3.MoveTowards(curPos, destPos, moveSpeed);
        rigid.MovePosition(deltaMove);
    }

    public static void PointMove(Transform target, Vector3 curPos, Vector3 destPos, float moveSpeed)
    {
        target.position = Vector3.MoveTowards(curPos, destPos, moveSpeed);
    }

    public static void Rotate(Transform target, Vector3 destRot, float rotateSpeed)
    {
        target.rotation = Quaternion.RotateTowards(target.rotation, Quaternion.LookRotation(destRot), rotateSpeed);
    }

    public static void RotateLook(Transform target, Vector3 destRot)
    {
        target.rotation = Quaternion.LookRotation(destRot);
    }

    public static void ForceDashMove(Rigidbody rigid, Transform trans, Vector3 dashDir, float dashPower, ForceMode mode)
    {
        rigid.AddForce(dashDir * dashPower, mode);
    }

    public static void TeleportMove(Transform trans, Vector3 teleDir, float telePower)
    {
        trans.Translate(teleDir * telePower, Space.World);
    }
}
