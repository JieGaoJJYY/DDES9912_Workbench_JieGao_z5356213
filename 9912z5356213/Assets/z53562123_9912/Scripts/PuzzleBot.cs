using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleBot : MonoBehaviour
{
    public BotSewing botSewing;
    public NavMeshAgent navMeshAgent;
    public Transform start;
    public Transform end;
    public Transform puzzleBotHand;
    public Transform sewingPiont;
    public MovableMagnetSnapper pizzle0;
    public Transform pizzle1;
    public MovableMagnetSnapper pizzle2;
    public Transform pizzle3;
    private void Update()
    {
        if(pizzle1.childCount !=0 &&  pizzle3.childCount != 0)
        {
            return;
        }

        if(botSewing.isFinsh == true)
        {
            if(puzzleBotHand.childCount != 0)
            {
                return ;
            }
            navMeshAgent.SetDestination(end.position);
            if (Vector3.Distance(transform.position, end.position) <= 1.03f)
            {
                if (sewingPiont.childCount == 0)
                {
                    return;
                }
                myCloth = sewingPiont.GetChild(0);
                myCloth.GetComponent<Movable>().enabled = false;
                Debug.Log(myCloth.name);
                sewingPiont.GetChild(0).position = puzzleBotHand.position;
                myCloth.parent = puzzleBotHand.transform;
                myCloth.localPosition = Vector3.zero;
                botSewing.isFinsh = false;
                Debug.Log("ÄÃ×ß");
            }
        }
        else
        {
            navMeshAgent.SetDestination(start.position);
            if (Vector3.Distance(transform.position, start.position) <= 1.03f)
            {
                myCloth = null;
                if (puzzleBotHand.childCount != 0)
                {
                    if (puzzleBotHand.GetChild(0).name == "1")
                    {
                        pizzle0.enabled = false;
                        pizzle2.enabled = false;
                        puzzleBotHand.GetChild(0).parent = pizzle1;
                        pizzle1.GetChild(0).localPosition = Vector3.zero;
                        pizzle1.GetChild(0).eulerAngles = Vector3.zero;

                        pizzle0.enabled = true;
                        pizzle2.enabled = true;
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
                }
            }
        }
    }

    private Transform myCloth;
    [ContextMenu("test")]
    public void Text()
    {
        myCloth.parent = puzzleBotHand;
        myCloth.localPosition = Vector3.zero;
    }
}
