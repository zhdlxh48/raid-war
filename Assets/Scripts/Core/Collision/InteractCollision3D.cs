using UnityEngine;

public class InteractCollision3D : MonoBehaviour
{
    public string colTag;

    private void OnParticleCollision(GameObject other)
    {
        OnPCollision(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(colTag))
            OnEnter(collision.gameObject);
    }

    public virtual void OnPCollision(GameObject go) { }

    public virtual void OnEnter(GameObject go) { }
}
