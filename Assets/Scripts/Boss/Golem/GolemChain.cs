using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemChain : MonoBehaviour
{
    Transform parentTransform;
    Rigidbody boneRigidbody;
    Vector3 prevFrameParentPosition = Vector3.zero;
    public float power = 0f;
    public float clampDist = 0.03f;

    private void Awake()
    {
        GetComponent<CharacterJoint>().connectedBody =
            transform.parent.GetComponent<Rigidbody>();

        parentTransform = transform.parent;
        prevFrameParentPosition = parentTransform.position;

        boneRigidbody = GetComponent<Rigidbody>();
       // boneRigidbody.AddForce(Vector3.up * 900, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Vector3 delta = (prevFrameParentPosition - parentTransform.position);
        delta.y = delta.y + power * 0.1f;
        delta.z = delta.z + power;
        boneRigidbody.AddForce(Vector3.ClampMagnitude(delta, clampDist) * power);

        prevFrameParentPosition = parentTransform.position;
    }
}
