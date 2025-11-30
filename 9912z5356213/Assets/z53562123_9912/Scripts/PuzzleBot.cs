using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleBot : MonoBehaviour
{
    public BotSewing botSewing;
    public NavMeshAgent navMeshAgent;
    public Transform start;//PuzleBot的起始位置
    public Transform end;//PuzleBot的终点位置
    public Transform puzzleBotHand;
    public Transform sewingPiont;
    public MovableMagnetSnapper pizzle0;//玩家放置编织完成的布料位置0
    public Transform pizzle1;//PuzleBot放置编织完成布料的位置1
    public MovableMagnetSnapper pizzle2;//玩家放置编织布料的位置2
    public Transform pizzle3;//PuzleBot放置编织完成布料的位置3
    //由于原始放置脚本来自于EZPZ，我无法让PuzleBot像玩家一样操作，因此我选择将PuzleBot放置的位置关闭原始脚本，直接使用坐标进行传递。
    private void Update()
    {
        if(pizzle1.childCount !=0 &&  pizzle3.childCount != 0)
            //当布料放置完成后，PuzleBot不再执行命令。
        {
            return;
        }

        if(botSewing.isFinsh == true)
        {
            if(puzzleBotHand.childCount != 0)
            {
                return ;
                //当puzzleBot手中有布料时，后续代码不执行。
            }
            navMeshAgent.SetDestination(end.position);
            if (Vector3.Distance(transform.position, end.position) <= 1.03f)
            {
                if (sewingPiont.childCount == 0)
                {
                    return;
                    //当缝纫机上没有布料的时候，后续代码不执行。
                }
                myCloth = sewingPiont.GetChild(0);
                myCloth.GetComponent<Movable>().enabled = false;
                //关掉放置布料容器上的脚本，放置发生位移。
                Debug.Log(myCloth.name);
                sewingPiont.GetChild(0).position = puzzleBotHand.position;
                //PuzzleBot拿走缝纫机上的布料
                myCloth.parent = puzzleBotHand.transform;
                myCloth.localPosition = Vector3.zero;
                botSewing.isFinsh = false;
                //拿走后缝纫机状态重置。
                Debug.Log("拿走");
            }
        }
        else
        {
            navMeshAgent.SetDestination(start.position);
            //PuzzleBot回到起始位置。
            if (Vector3.Distance(transform.position, start.position) <= 1.03f)
            {
                myCloth = null;
                //重置Cloth
                if (puzzleBotHand.childCount != 0)
                {
                    if (puzzleBotHand.GetChild(0).name == "1")
                        //puzzleBot将布料放置在该有的位置上，同时将布料的位置重置。
                    {
                        pizzle0.enabled = false;
                        pizzle2.enabled = false;
                        puzzleBotHand.GetChild(0).parent = pizzle1;
                        pizzle1.GetChild(0).localPosition = Vector3.zero;
                        pizzle1.GetChild(0).eulerAngles = Vector3.zero;

                        pizzle0.enabled = true;
                        pizzle2.enabled = true;
                        //玩家放置布料的容器可能会在PizzleBot放置布料时将布料吸附过去，因此暂时关闭位置0和位置2上的组件
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
                    //为了保证缝纫机上的布料可以放置在puzzleBotHand中
                }
            }
        }
    }

    private Transform myCloth;
    //PuzzleBot运送的布料
   
    public void Text()
    {
        myCloth.parent = puzzleBotHand;
        myCloth.localPosition = Vector3.zero;
    }
}
