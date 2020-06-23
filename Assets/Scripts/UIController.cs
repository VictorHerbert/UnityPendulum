using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Time")]
    public TextMeshProUGUI timeText;
    public Slider timeSlider;
    
    [Header("Button")]
    public Image PlayImage;
    public Image PauseImage;

    [Header("Scale")]
    public TMP_InputField timeInput;

    [Header("Chart")]
    public TimeGraph graph;
    [Range(0f,100f)]
    public float chartScale;


#region Slider

    public void setTime(string time){
        timeText.text = time;
    }

    public void setTimeInput(float time){
        timeInput.text = time.ToString();
    }

    public void setSliderRange(int begin, int end){
        timeSlider.minValue = begin;
        timeSlider.maxValue = end;
    }

    public void setSliderPos(int pos){
        timeSlider.value = pos;
    }

    public int getSliderPos(){
        return (int) timeSlider.value;
    }

    public void setPlay(bool isPlaying){
        PlayImage.gameObject.SetActive(!isPlaying);
        PauseImage.gameObject.SetActive(isPlaying);
    }

#endregion

#region TimeScale

    public float getTimeScale(){
        
        float f;
        float.TryParse(timeInput.text.Replace('.',','),out f);
        
        if(f != 0 && f < 101)
            return f;
        else
            return 1.0f;
    }

#endregion    

#region Chart

    public void plotPush(int id, float v){
        graph.PushData(id,v*chartScale);
    }

#endregion

}
