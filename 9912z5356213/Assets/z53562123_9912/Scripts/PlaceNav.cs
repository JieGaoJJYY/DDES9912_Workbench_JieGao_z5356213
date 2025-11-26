using UnityEngine;
using UnityEngine.AI;

public class PlaceNav : MonoBehaviour
{
    //仓库NPC
    //公开那三个点
    //公开NPC上的Nav组件
    //当传送带上没有布的时候，NPC，先从停止位到桌子旁边，拿起布，从桌子旁到传送带，放置布，回到停止位。
    //公开传送带。如果传送带没有子物体，就说明， 没有布。
    public Transform t_Start;
    public Transform t_Mid;
    public Transform t_End;
    public NavMeshAgent agent;
    public Transform conveyor;//传送带
    public Transform presonHand;//NPC手里
    //NPC手里没有布以及，传送带上没有布的时候去mid
    //NPC手里有布,传送带上没有布，就去End
    //传送带上有布就是Start
    public Transform[] cloth;

    private void Update()
    {
        if(conveyor.childCount == 0 && presonHand.childCount == 0)
        {
            agent.SetDestination(t_Mid.position);
            

            if (Vector3.Distance(transform.position, t_Mid.position) <= 1.03f)
            {
                int index = Random.Range(0, cloth.Length);
                Transform t = Instantiate(cloth[index], presonHand);
                t.localPosition = Vector3.zero;
            }
        }
        else if (presonHand.childCount>0 && conveyor.childCount == 0)
        {
            agent.SetDestination(t_End.position);
            if (Vector3.Distance(transform.position, t_End.position) <= 1.03f)
            {
                Transform t = presonHand.GetChild(0);
                t.parent = conveyor;
                t.localPosition = Vector3.zero;
            }
        }
        else if (conveyor.childCount > 0)
        {
            agent.SetDestination(t_Start.position);

        }
    }


}
