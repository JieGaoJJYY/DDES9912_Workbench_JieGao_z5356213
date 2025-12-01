using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleBot : MonoBehaviour
{
    public BotSewing botSewing;
    public NavMeshAgent navMeshAgent;
    public Transform start;//PuzleBot's starting position
    public Transform end;//PuzleBot's final destination
    public Transform puzzleBotHand;
    public Transform sewingPiont;
    public MovableMagnetSnapper pizzle0;//Player places the woven fabric at position 0
    public Transform pizzle1;//PuzleBot Placement of Woven Fabric Position 1
    public MovableMagnetSnapper pizzle2;//Player placement location for woven fabric 2
    public Transform pizzle3;//PuzleBot Placement: Finished Fabric Position 3
    //Since the original placement script originated from EZPZ,
    //I couldn't make PuzleBot operate like a player. Therefore,
    //I chose to disable the original script at the placement location and directly pass coordinates instead.
    private void Update()
    {
        if(pizzle1.childCount !=0 &&  pizzle3.childCount != 0)
        //When cloth placement is complete, PuzleBot will no longer execute commands.
        {
            return;
        }

        if(botSewing.isFinsh == true)
        {
            if(puzzleBotHand.childCount != 0)
            {
                return ;
                //When puzzleBot has cloth in its hand, subsequent code does not execute.
            }
            navMeshAgent.SetDestination(end.position);
            if (Vector3.Distance(transform.position, end.position) <= 1.03f)
            {
                if (sewingPiont.childCount == 0)
                {
                    return;
                    //When there is no cloth on the sewing machine, subsequent code does not execute.
                }
                myCloth = sewingPiont.GetChild(0);
                myCloth.GetComponent<Movable>().enabled = false;
                //Close the script on the fabric container to prevent displacement during placement.
                Debug.Log(myCloth.name);
                sewingPiont.GetChild(0).position = puzzleBotHand.position;
                //PuzzleBot removes the cloth from the sewing machine.
                myCloth.parent = puzzleBotHand.transform;
                myCloth.localPosition = Vector3.zero;
                botSewing.isFinsh = false;
                //After removal, the sewing machine status will reset.
                Debug.Log("ÄÃ×ß");
            }
        }
        else
        {
            navMeshAgent.SetDestination(start.position);
            //PuzzleBot returns to its starting position.
            if (Vector3.Distance(transform.position, start.position) <= 1.03f)
            {
                myCloth = null;
                //Reset Cloth
                if (puzzleBotHand.childCount != 0)
                {
                    if (puzzleBotHand.GetChild(0).name == "1")
                    //puzzleBot positions the cloth where it should be while simultaneously resetting the cloth's position.
                    {
                        pizzle0.enabled = false;
                        pizzle2.enabled = false;
                        puzzleBotHand.GetChild(0).parent = pizzle1;
                        pizzle1.GetChild(0).localPosition = Vector3.zero;
                        pizzle1.GetChild(0).eulerAngles = Vector3.zero;

                        pizzle0.enabled = true;
                        pizzle2.enabled = true;
                        //The container where players place fabric may attract the fabric when PizzleBot places it,
                        //so components at positions 0 and 2 are temporarily disabled.
                    }
                    else if (puzzleBotHand.GetChild(0).name == "3")
                    {
                        pizzle0.enabled = false;
                        pizzle2.enabled = false;
                        puzzleBotHand.GetChild(0).parent = pizzle3;
                        pizzle3.GetChild(0).localPosition = Vector3.zero;
                        pizzle3.GetChild(0).eulerAngles = Vector3.zero;

                        pizzle0.enabled = true;
                        pizzle2.enabled = true;
                    }
                }
            }
            else {
                if (myCloth != null)
                {
                    Text();
                    //To ensure the fabric on the sewing machine can be placed in the puzzleBotHand
                }
            }
        }
    }

    private Transform myCloth;
    //Fabric delivered by PuzzleBot

    public void Text()
    {
        myCloth.parent = puzzleBotHand;
        myCloth.localPosition = Vector3.zero;
    }
}
