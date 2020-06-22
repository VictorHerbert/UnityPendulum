using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PendulumMovement : MonoBehaviour
{

    [Header("Game Objects")]
    public Transform sphere1;
    public Transform sphere2;
    public Transform sphere3;
    List<Vector3> pos1 = new List<Vector3>();
    List<Vector3> pos2 = new List<Vector3>();
    List<Vector3> pos3 = new List<Vector3>();
    List<string> times = new List<string>();

    public int k;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public Button PlayButton;
    public Image PlayImage;
    public Image PauseImage;
    public Slider timeSlider;
    public TMP_InputField timeInput;

    public TimeGraph graph;

    public void setTimeStep(){
        float f;
        float.TryParse(timeInput.text.Replace('.',','),out f);
        if(f != 0 && f < 101)
            k = (int) (100/f);
        Debug.Log(k);
        
    }

    StreamReader reader;

    void Start()
    {
        reader = new StreamReader(@"positions.csv");
        timeInput.text = ((float) 100/k).ToString();
        //Debug.Log((float) 1/k);
        StartCoroutine(readFile());    
        StartCoroutine(play());    
    }
    
    IEnumerator readFile()
    {
        while (reader.Peek() >= 0){
            string[] values = reader.ReadLine().Split(';');

            times.Add(values[0]);

            pos1.Add(
                new Vector3(
                    float.Parse(values[1]),
                    float.Parse(values[2]),
                    0
                )
            );

            pos2.Add(
                new Vector3(
                    float.Parse(values[3]),
                    float.Parse(values[4]),
                    0
                )
            );

            pos3.Add(
                new Vector3(
                    float.Parse(values[5]),
                    float.Parse(values[6]),
                    0
                )
            );
        }   

        timeSlider.minValue = 0;
        timeSlider.maxValue = times.Count-1;
        
        yield return null;
    }

    bool isPlaying;

    int v;

    public void togglePlay(){
        isPlaying = !isPlaying;

        PlayImage.gameObject.SetActive(!isPlaying);
        PauseImage.gameObject.SetActive(isPlaying);
    }


    public void setPos(){
        pos = (int) timeSlider.value;
        
        timeSlider.value = pos;

        sphere1.position = pos1[pos];
        sphere2.position = pos2[pos];
        sphere3.position = pos3[pos];

        timeText.text = times[pos];
        
    }
        

    int pos;

    IEnumerator play()
    {
        while(true){
            if(isPlaying){
                if(pos < times.Count){
                    timeSlider.value = pos;

                    sphere1.position = pos1[pos];
                    sphere2.position = pos2[pos];
                    sphere3.position = pos3[pos];

                    timeText.text = times[pos];
                    
                    pos = pos + k;

                    graph.PushData(0,pos2[pos].y);
                    graph.PushData(1,pos3[pos].y);
                }
                
            }            

            yield return new WaitForSeconds((float) 0.001/k);

        }   

    }

}

