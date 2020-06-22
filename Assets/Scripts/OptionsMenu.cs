using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI")]    
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;

    [Header("GameObjects")]    
    public TrailRenderer trail1;
    public TrailRenderer trail2;
    public TrailRenderer trail3;
    bool[] trail1Active = {true,true,true};
    

    public void toggleTrail(int toggleIndex){
        trail1Active[toggleIndex-1] = !trail1Active[toggleIndex-1];

        trail1.enabled = (trail1Active[0]);
        trail2.enabled = (trail1Active[1]);
        trail3.enabled = (trail1Active[2]);        
    }

    bool isActive;
    public void hideMenu(){
        isActive = !isActive;

        GetComponent<Animator>().SetBool("Active",isActive);
        
    }
    
    void Update()
    {
        if(Input.GetKey("s")){
            ScreenCapture.CaptureScreenshot("Cap.png");
            Debug.Log("Foi");
        }
    }
}
