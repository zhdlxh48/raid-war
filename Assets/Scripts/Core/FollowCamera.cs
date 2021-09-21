using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;

    /// <summary> 카메라가 따라갈 때의 높이 </summary>
    public float zFollowDist = 4.0f;
    /// <summary> 카메라가 따라갈 때의 높이 </summary>
    public float yFollowHeight = 3.5f;
    /// <summary> z축을 기준으로 도는 속도 </summary>
    public float yRotSpeed = 150.0f;
    /// <summary> Target을 따라가는 속도 </summary>
    public float followSpeed;

    /////////////////////////////////
    // Shake Cam Value
    /////////////////////////////////
    [Range(0, 1.5f)]
    public float ShakeRadius = 0.3f;
    public float ShakeTime = 0.5f;

    private Vector2 shakeValue;
    private Vector3 pos;
    private float time;
    /////////////////////////////////

    private float followMarginRange = 0.05f;

    private void Awake()
    {
        if (followTarget == null)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player").transform.root;
        }
        shakeValue = Vector2.zero;
    }

    private void Start()
    {
        StartCoroutine(StartFollowTarget());
    }

    private void FixedUpdate()
    {
        if ((followTarget.position - transform.position).sqrMagnitude >= followMarginRange * followMarginRange)
        {
            pos.x = Mathf.Lerp(transform.position.x, followTarget.position.x, followSpeed * Time.deltaTime);
            pos.y = Mathf.Lerp(transform.position.y, yFollowHeight, followSpeed * Time.deltaTime);
            pos.z = Mathf.Lerp(transform.position.z, followTarget.position.z - zFollowDist, followSpeed * Time.deltaTime);
        }

        pos.x += shakeValue.x + atkSakeVec.x;
        pos.y += shakeValue.y + atkSakeVec.y;

        transform.position = pos;
    }

    Vector3 startPos;

    private IEnumerator StartFollowTarget()
    {
        startPos = new Vector3(followTarget.position.x, yFollowHeight, followTarget.position.z - zFollowDist);
        //Debug.Log("Start Moving");

        WaitForSeconds delay = new WaitForSeconds(0.002f);

        //bool[] bCheck = new bool[2] { false, false };

        Vector3 dir = startPos - transform.position;

        while (dir.sqrMagnitude < followMarginRange * followMarginRange)
        {
            MovementUtil.PointMove(transform, transform.position, startPos, followSpeed * Time.deltaTime);

            yield return delay;
        }
        //Debug.Log("End Moving");

        yield break;
    }

    /// <summary>
    /// Shake Cam
    /// </summary>
    public void shake() 
    {
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        time = 0;
        while (time <= ShakeTime)
        {
            shakeValue = (Vector2)UnityEngine.Random.insideUnitCircle * ShakeRadius;
            time += Time.deltaTime;
            yield return null;
        }
        shakeValue.x = 0;
        shakeValue.y = 0;
    }

    // Attack Shake
    Vector2 atkSakeVec;
    float atkShakeTime;
    public void AttackShake()
    {
        StartCoroutine(AttackShakeCamera());
    }

    WaitForSeconds atkWaitTime = new WaitForSeconds(0.015f);
    IEnumerator AttackShakeCamera()
    {
        atkShakeTime = 0;
        yield return atkWaitTime;
        while (atkShakeTime <= 0.05f)
        {
            atkSakeVec = (Vector2)UnityEngine.Random.insideUnitCircle * 0.3f;
            atkShakeTime += Time.deltaTime;
            yield return null;
        }
        atkSakeVec.x = 0;
        atkSakeVec.y = 0;
    }
}
