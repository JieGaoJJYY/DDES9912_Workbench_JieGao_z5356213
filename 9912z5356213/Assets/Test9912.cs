using UnityEngine;

public class Test9912 : MonoBehaviour
{
    public float force = 50;
    void Start()
    {
        
    }

    
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * force);
    }
}
