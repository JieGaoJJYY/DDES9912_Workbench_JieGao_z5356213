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
    private int index = 0;
    private void Update()
    {
        if(index > 4)
        //一件衣服需要四块布料。当凑齐4块布料的时候，仓库就停止传送布料。
        {
            return;
        }
        if(conveyor.childCount == 0 && presonHand.childCount == 0)
            //当游戏开始时，或传送带上没有布料的时候，仓库NPC就去拿起布料。
        {
            agent.SetDestination(t_Mid.position);
            //设置仓库NPC向布料的坐标移动。
            

            if (Vector3.Distance(transform.position, t_Mid.position) <= 1.03f)//因为人物和地面位置有偏移，偏移大概是1.02f,所以当他们的距离小于1.03时。
                //当仓库NPC走到布料坐标时，复制一块桌子上的布料，
            {
                
                Transform t = Instantiate(cloth[index], presonHand);
                //新复制的布料以PresonHand为父物体。
                t.localPosition = Vector3.zero;
                //单独移动父物体后还需要将物体坐标归零。
                t.name = index.ToString();
                //因为后续需要将布拼起来，因此需要记录每一块布的名称。为后续合并布料提供方便。
                index++;

            }
        }
        else if (presonHand.childCount>0 && conveyor.childCount == 0)
            //当PresonHand有布料，并且传送带上没有子物体。
        {
            agent.SetDestination(t_End.position);
            //仓库NPC就需要走到传送带附近
            if (Vector3.Distance(transform.position, t_End.position) <= 1.03f)
                //当仓库NPC走到传送带附近时，放置PresonHand的子物体到Conveyor的子物体中。并且坐标归零。
            {
                Transform t = presonHand.GetChild(0);
                t.parent = conveyor;
                t.localPosition = Vector3.zero;
            }
        }
        else if (conveyor.childCount > 0)
        {
            agent.SetDestination(t_Start.position);
            //如果conveyor上的子物体大于零，仓库NPC就回到起始位。

        }
    }


}
