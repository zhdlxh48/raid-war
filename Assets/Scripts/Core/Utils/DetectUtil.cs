using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectUtil
{
    public static Collider[] DetectObjectWithPhysicsSphere(Vector3 center, float radius, LayerMask mask)
    {
        return Physics.OverlapSphere(center, radius, mask);
    }
    public static Collider[] DetectObjectsWithPhysicsSphere(Vector3 center, float radius, LayerMask[] masks)
    {
        List<Collider> detectObjects = null;
        foreach (LayerMask mask in masks)
        {
            detectObjects.AddRange(Physics.OverlapSphere(center, radius, mask));
        }
        
        return detectObjects.ToArray();
    }

    public static Transform[] DetectObjectsTransformWithAngle(Transform[] transfList, Transform player, float detectAngle, float detectDistance)
    {
        Vector3 _leftBoundary = MathUtil.AngleToDirectionVector(-detectAngle * 0.5f, player.eulerAngles.y);
        Vector3 _rightBoundary = MathUtil.AngleToDirectionVector(detectAngle * 0.5f, player.eulerAngles.y);

        Debug.DrawRay(player.position + player.up, _leftBoundary.normalized * detectDistance, Color.red);
        Debug.DrawRay(player.position + player.up, _rightBoundary.normalized * detectDistance, Color.red);

        // 범위 내의 Enemy들 Detect
        List<Transform> targets = new List<Transform>();
        int len = transfList.Length;

        int num = 0;

        for (int i = 0; i < len; i++)
        {
            if (Vector3.Distance(player.position, transfList[i].position) <= detectDistance)
            {
                float enemyAngle = MathUtil.DirectionVectorToAngle(player.position, transfList[i].position, player.forward);
                // TODO: HERE
                if (enemyAngle < detectAngle * 0.5f)
                {
                    num++;
                    targets.Add(transfList[i]);
                    Debug.DrawRay(player.position + player.up, transfList[i].position - player.position + player.up, Color.blue);
                }
            }
        }

        return targets.ToArray();
    }

    public static void DebugDrawAngle(Transform[] transfList, Transform player, float detectAngle, float detectDistance)
    {
        Vector3 _leftBoundary = MathUtil.AngleToDirectionVector(-detectAngle * 0.5f, player.eulerAngles.y);
        Vector3 _rightBoundary = MathUtil.AngleToDirectionVector(detectAngle * 0.5f, player.eulerAngles.y);

        Debug.DrawRay(player.position + player.up, _leftBoundary.normalized * detectDistance, Color.red);
        Debug.DrawRay(player.position + player.up, _rightBoundary.normalized * detectDistance, Color.red);

        for (int i = 0; i < transfList.Length; i++)
        {
            if (Vector3.Distance(player.position, transfList[i].position) <= detectDistance)
            {
                float enemyAngle = MathUtil.DirectionVectorToAngle(player.position, transfList[i].position, player.forward);
                // TODO: HERE
                if (enemyAngle < detectAngle * 0.5f)
                {
                    Debug.DrawRay(player.position + player.up, transfList[i].position - player.position + player.up, Color.blue);
                }
            }
        }
    }
}
