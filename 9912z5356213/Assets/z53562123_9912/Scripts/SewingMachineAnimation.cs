using UnityEngine;
using UnityEngine.UI;

public class SewingMachineAnimation : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isfinsh = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cloth")
        {
            other.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount += 0.1f;
            int result = (int)(other.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount * 100f);
            other.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = result.ToString()+"%";
            if(other.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount == 1f && isfinsh == false)
            {
                audioSource.Play();
                isfinsh = true;
            }
            else if(other.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount<1)
            {
                isfinsh = false;
            }
            
        }
        
    }
}
