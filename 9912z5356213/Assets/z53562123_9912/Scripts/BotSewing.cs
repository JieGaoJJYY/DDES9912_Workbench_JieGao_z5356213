using UnityEngine;
using UnityEngine.UI;

public class BotSewing : MonoBehaviour
{
    public Animator animator;
    //动画脚本
    public Transform clothParent;
    //缝纫机上放置布料的地方

    //由于缝纫机动力来源是齿轮，控制齿轮的按钮需要通过鼠标点击触发。因此，需要再Bot身上的脚本中调用点击事件。
    //公开InteractableGeneral脚本。
    //模拟鼠标每一秒点击一次按钮。
    public InteractableGeneral interactableGeneral;
    private float time = 1f;//限制缝纫机NPC点击的频率为一秒钟一次。
    private float timer = 1f;
    public InteractableGeneral interactableGeneralStop;
    public bool isFinsh = false;
    private void Update()
    {
        if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text != "100%")
            //当NPC缝纫机上放置的布料图案编织未完成时
        {
            animator.SetBool("start", true);
            animator.SetBool("stop", false);
           
            timer -= Time.deltaTime;
            //缝纫NPC每秒钟给缝纫机增加一次动力。
            if (timer < 0)
            {
                timer = time;
                interactableGeneral.onPrimaryInteract.Invoke();
                //调用点击事件的方法。
                //Debug.Log("缝纫机开工");
            }
            isFinsh = false;
        }
        else if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text == "100%")
            //图案编织完成后
        {
            animator.SetBool("start", false);
            animator.SetBool("stop", true);
            interactableGeneralStop.onPrimaryInteract.Invoke();
            timer = time;
            isFinsh = true;
        }
        
        else
        {
            timer = time;
            isFinsh = false;
        }

        
    }
}
