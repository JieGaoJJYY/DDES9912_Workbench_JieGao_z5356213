using System.Timers;
using UnityEngine;

public class ClothesMove : MonoBehaviour
{ 
    public Transform midt;
    public Transform t1;
    public Transform t2;
    private Transform aim;
    public Transform clothes;
    public bool isMid = false;
    
    private void Start()
    {

        aim = t1;
    }
    private void Update()
    {
       if(clothes == null && transform.childCount != 0)
        {
            clothes = transform.GetChild(0);
        }
        else if(clothes!=null)
        {
            if(isMid == false)
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, midt.position, 2f * Time.deltaTime);
                clothes.position = vector3;
                if(Vector3.Distance(clothes.position, midt.position) <= 0.01f)
                {
                    isMid = true;
                }
            }
            else
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, aim.position, 2f * Time.deltaTime);
                clothes.position = vector3;
                if (Vector3.Distance(clothes.position, aim.position) <= 0.01f)
                {
                    Debug.Log("×ßµ½");
                    clothes.parent = null;
                    clothes = null;
                    isMid = false;
                    if(aim == t1)
                    {
                        aim = t2;
                    }
                    else
                    {
                        aim = t1;
                    }
                }
            }
        }
    }
}
