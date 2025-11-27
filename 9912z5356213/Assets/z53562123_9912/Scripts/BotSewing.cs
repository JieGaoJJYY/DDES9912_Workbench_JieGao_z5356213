using UnityEngine;
using UnityEngine.UI;

public class BotSewing : MonoBehaviour
{
    public Animator animator;
    public Transform clothParent;

    //由于缝纫机动力来源是齿轮，控制齿轮的按钮需要通过鼠标点击触发。因此，需要再Bot身上的脚本中调用点击事件。
    //公开InteractableGeneral脚本。
    //模拟鼠标每一秒点击一次按钮。
    public InteractableGeneral interactableGeneral;
    private float time = 1f;
    private float timer = 1f;
    public InteractableGeneral interactableGeneralStop;
    private void Update()
    {
        if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text != "100%")
        {
            animator.SetBool("start", true);
            animator.SetBool("stop", false);
           
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = time;
                interactableGeneral.onPrimaryInteract.Invoke();
                //Debug.Log("缝纫机开工");
            }
        }
        else if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text == "100%")
        {
            animator.SetBool("start", false);
            animator.SetBool("stop", true);
            interactableGeneralStop.onPrimaryInteract.Invoke();
            timer = time;
        }
        else
        {
            timer = time;
        }

        
    }
}
