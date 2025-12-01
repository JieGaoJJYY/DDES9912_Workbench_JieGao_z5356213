using System.Timers;
using UnityEngine;

public class ClothesMove : MonoBehaviour
{ 
    public Transform midt;
    //Three coordinates are publicly disclosed, representing three positions on the conveyor belt.
    public Transform t1;
    public Transform t2;
    private Transform aim;
    public Transform clothes;//Cloth moving along the conveyor belt
    public bool isMid = false;
    //Publicize a Boolean value indicating whether the fabric
    //has reached the midpoint on the conveyor belt.

    private void Start()
    {

        aim = t1;
        //The warehouse NPC will first deliver the fabric to the player.
    }
    private void Update()
    {
       if(clothes == null && transform.childCount != 0)
        //There are currently no garments being conveyed,
        //and there are sub-objects on the conveyor belt.
        {
            clothes = transform.GetChild(0);
            //At this time, the child object is the garment being transferred.
        }
        else if(clothes!=null)
        {
            if(isMid == false)
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, midt.position, 2f * Time.deltaTime);
                //At this time, the clothes move toward Midt. Set the distance to move per frame.
                //Calculate the current position of the movement.
                clothes.position = vector3;
                if(Vector3.Distance(clothes.position, midt.position) <= 0.01f)
                //Determine whether the clothes have reached the midpoint.
                {
                    isMid = true;
                }
            }
            else
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, aim.position, 2f * Time.deltaTime);
                //The current clothes are moving toward the Aim coordinates,
                //where Aim could be either t1 or t2.
                clothes.position = vector3;
                if (Vector3.Distance(clothes.position, aim.position) <= 0.01f)
                {
                    //Debug.Log("×ßµ½");
                    if(aim == t2 && botSewingParent.childCount == 0)
                    {
                        BotTakeCloth();
                    }
                    
                }
            }
        }
  }
    //------------------------------------------------------------------------ 
    //The bot automatically takes the cloth.
    public Transform botSewingParent;
    public void BotTakeCloth()
    {
        
        clothes.parent = botSewingParent;
        clothes = null;
        isMid = false;
        aim = t1;
        botSewingParent.GetChild(0).localPosition = Vector3.zero;
        //When the clothes reach t2,
        //change the parent object of Clothes to botSewingParent and reset its coordinates to zero.


    }
    //Players need to click on the cloth to take it.
    public void PlayerTakeCloth()
    {

        if (clothes == null || aim== t2||clothes.name=="1"||clothes.name=="3") return;
        //Players are responsible for completing fabrics 1 and 3.
        //The remaining fabrics 2 and 4 are completed by an NPC.
        clothes.parent = null;
        clothes = null;
        isMid = false;
        aim = t2;
    }
}   
