using UnityEngine;
using UnityEngine.InputSystem;

public class HandForce : MonoBehaviour
{
    public int force = 0;
    private Vector3 pos = new Vector3();
    private void Update()
    {
     if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            pos = Mouse.current.position.ReadValue();
        }
     else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            float d = Mouse.current.position.ReadValue().y - pos.y;
            if (d > 0)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * force);
            }
            else
            {

                GetComponent<Rigidbody>().AddForce(Vector3.down * force);
            }
        }
    }
}
