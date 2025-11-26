using UnityEngine;

public class SewingStop : MonoBehaviour
{
    public Rigidbody rigidbody0;

    public void Stop()
    {
        rigidbody0.angularDamping = 1000f;
    }
    public void Begin()
    {
        rigidbody0.angularDamping = 0.025f;
    }
}
