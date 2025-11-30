using System.Timers;
using UnityEngine;

public class ClothesMove : MonoBehaviour
{ 
    public Transform midt;
    //公开三个坐标，分别是传送带上的三个位置。
    public Transform t1;
    public Transform t2;
    private Transform aim;
    public Transform clothes;//正在传送带上移动的布料
    public bool isMid = false;
    //公开一个布尔值，判定布料有没有到达传送带上的中间位置。
    
    private void Start()
    {

        aim = t1;
        //仓库NPC首先将布料传送给玩家。
    }
    private void Update()
    {
       if(clothes == null && transform.childCount != 0)
            //当前没有正在传送的衣服，并且传送带上有子物体。
        {
            clothes = transform.GetChild(0);
            //此时，子物体为正在传送的衣服。
        }
        else if(clothes!=null)
        {
            if(isMid == false)
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, midt.position, 2f * Time.deltaTime);
                //此时clothes向Midt进行移动。设置每帧移动的距离。计算当前移动的位置。
                clothes.position = vector3;
                if(Vector3.Distance(clothes.position, midt.position) <= 0.01f)
                    //判断clothes是否走到了midt。
                {
                    isMid = true;
                }
            }
            else
            {
                Vector3 vector3 = Vector3.MoveTowards(clothes.position, aim.position, 2f * Time.deltaTime);
                //当前的clothes走向Aim的坐标，aim有可能是t1或是t2.
                clothes.position = vector3;
                if (Vector3.Distance(clothes.position, aim.position) <= 0.01f)
                {
                    //Debug.Log("走到");
                    if(aim == t2 && botSewingParent.childCount == 0)
                    {
                        BotTakeCloth();
                    }
                    
                }
            }
        }
  }
    //------------------------------------------------------------------------ 
    //Bot拿走布是自动执行的
    public Transform botSewingParent;
    public void BotTakeCloth()
    {
        
        clothes.parent = botSewingParent;
        clothes = null;
        isMid = false;
        aim = t1;
        botSewingParent.GetChild(0).localPosition = Vector3.zero;
        //当clothes走到了t2时，将Clothes的父物体改为botSewingParent,并且坐标归零。
            
        
    }
    //玩家需要点击布才能拿走布
    public void PlayerTakeCloth()
    {

        if (clothes == null || aim== t2||clothes.name=="1"||clothes.name=="3") return;
        //玩家负责完成1和3布料。剩下的2和4布料由NPC完成。
        clothes.parent = null;
        clothes = null;
        isMid = false;
        aim = t2;
    }
}   
