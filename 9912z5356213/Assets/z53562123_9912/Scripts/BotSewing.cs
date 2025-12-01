using UnityEngine;
using UnityEngine.UI;

public class BotSewing : MonoBehaviour
{
    public Animator animator;
    //Animation Component
    public Transform clothParent;
    //The place where cloth is placed on the sewing machine
    //Since the sewing machine's power source is a gear,
    //the button controlling the gear must be triggered by a mouse click.
    //Therefore, the click event needs to be invoked within the script on the Bot.
    //Publicly release the InteractableGeneral script.
    //Simulate a mouse clicking its button once per second.
    public InteractableGeneral interactableGeneral;
    private float time = 1f;//Limit NPC clicks on the sewing machine to once per second.
    private float timer = 1f;
    public InteractableGeneral interactableGeneralStop;
    public bool isFinsh = false;
    private void Update()
    {
        if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text != "100%")
        //When the cloth pattern on the NPC sewing machine is not fully woven
        {
            animator.SetBool("start", true);
            animator.SetBool("stop", false);
           
            timer -= Time.deltaTime;
            //The sewing NPC adds power to the sewing machine once per second.
            if (timer < 0)
            {
                timer = time;
                interactableGeneral.onPrimaryInteract.Invoke();
                //Method for invoking click events.
                //Debug.Log("·ìÈÒ»ú¿ª¹¤");
            }
            isFinsh = false;
        }
        else if (clothParent.childCount != 0 && clothParent.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text == "100%")
        //After the pattern is woven
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
