using UnityEngine;
using UnityEngine.AI;

public class PlaceNav : MonoBehaviour
{
    //Warehouse NPC
    //Public three transform
    //Publicizing the Nav component on the NPC
    //When there is no cloth on the conveyor belt,
    //the NPC first moves from the stop position to the table, picks up the cloth,
    //moves from the table to the conveyor belt,
    //places the cloth, and then returns to the stop position.
    //Public conveyor belt. If the conveyor belt has no child objects, it indicates that there is no cloth.
    public Transform t_Start;
    public Transform t_Mid;
    public Transform t_End;
    public NavMeshAgent agent;
    public Transform conveyor;//conveyor belt
    public Transform presonHand;//In the hands of the NPC
    //When an NPC has no cloth and there is no cloth on the conveyor belt, go to mid.
    //If the NPC has cloth but the conveyor belt is empty, go to End.
    //Go to Start when there is fabric on the conveyor belt.
    public Transform[] cloth;
    private int index = 0;
    private void Update()
    {
        if(index > 4)
        //One garment requires four pieces of fabric. Once all four pieces are gathered,
        //the warehouse will cease delivering fabric.
        {
            return;
        }
        if(conveyor.childCount == 0 && presonHand.childCount == 0)
        //When the game begins, or when there is no fabric on the conveyor belt,
        //the warehouse NPC will pick up the fabric.
        {
            agent.SetDestination(t_Mid.position);
            //Set the warehouse NPC to move to the coordinates of the fabric.


            if (Vector3.Distance(transform.position, t_Mid.position) <= 1.03f)
            //Due to the offset between characters and the ground position, which is approximately 1.02f,
            //when their distance is less than 1.03.
            //When the warehouse NPC reaches the fabric coordinates,
            //duplicate a piece of fabric from the table.
            {

                Transform t = Instantiate(cloth[index], presonHand);
                //The newly duplicated fabric has PresonHand as its parent object.
                t.localPosition = Vector3.zero;
                //After moving the parent object independently,
                //you must also reset the object's coordinates to zero.
                t.name = index.ToString();
                //Since the fabrics will need to be pieced together later,
                //it is necessary to record the name of each fabric piece.
                //This facilitates the subsequent merging of the fabrics.
                index++;

            }
        }
        else if (presonHand.childCount>0 && conveyor.childCount == 0)
        //When PresonHand has fabric and there are no sub-objects on the conveyor belt.
        {
            agent.SetDestination(t_End.position);
            //The warehouse NPC needs to walk near the conveyor belt.
            if (Vector3.Distance(transform.position, t_End.position) <= 1.03f)
            //When the warehouse the NPC approaches the conveyor belt,
            //place the sub-object of PresonHand into the sub-object of Conveyor.
            //Additionally, reset the coordinates to zero.
            {
                Transform t = presonHand.GetChild(0);
                t.parent = conveyor;
                t.localPosition = Vector3.zero;
            }
        }
        else if (conveyor.childCount > 0)
        {
            agent.SetDestination(t_Start.position);
            //If the number of sub-objects on the conveyor is greater than zero,
            //the warehouse NPC returns to its starting position.

        }
    }


}
