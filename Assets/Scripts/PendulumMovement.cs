using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMovement : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] string fileName;

    [Header("Game Objects")]
    [SerializeField] Transform[] spheres;

    [Header("Stats")]
    public int time;
    [Range(1.0f,100.0f)]
    public int timeScale;
    public bool isPlaying;
    
    
    [Header("Components")]
    [SerializeField] UIController uiController;

#region Data

    List<List<Vector3>> pos = new List<List<Vector3>>();
    List<string> times = new List<string>();

#endregion
    
#region File Input

    StreamReader reader;

    IEnumerator readFile()
    {
        while (reader.Peek() >= 0){
            string[] values = reader.ReadLine().Split(';');

            times.Add(values[0].Substring(0,Mathf.Min(values[0].Length,5)));

            for(int i = 0; i < pos.Count; i++){
                pos[i].Add(
                    new Vector3(
                        float.Parse(values[2*i+1]),
                        float.Parse(values[2*i+2]),
                        0
                    )
                );
            }

        }   

        uiController.setSliderRange(0,times.Count-1);
        
        yield return null;
    }

#endregion

#region  Game Logic

    void Start()
    {
        reader = new StreamReader(fileName);
        uiController.setTimeInput((float) 100/timeScale); 

        for(int i = 0; i < spheres.Length; i++){
            pos.Add(new List<Vector3>());
        }

        StartCoroutine(readFile());    
        StartCoroutine(play()); 
        Debug.Log(pos[1][4]);
    }


    public void togglePlay()
    {
        isPlaying = !isPlaying;
        uiController.setPlay(isPlaying);
    }

    public void setTimeStep(){
        timeScale = (int) (100/uiController.getTimeScale());
    }

    public void setPos()
    {
        time = uiController.getSliderPos();
        uiController.setSliderPos(time);

        for(int i = 0; i < spheres.Length; i++){
            spheres[i].position = pos[i][time];
        }

        uiController.setTime(times[time]);
    }
        

    IEnumerator play()
    {
        while(true){
            if(isPlaying){
                if(time < times.Count){
                    uiController.setSliderPos(time);

                    for(int i = 0; i < spheres.Length; i++){
                        spheres[i].position = pos[i][time];
                        uiController.plotPush(i,Mathf.Atan(pos[i][time].y/pos[i][time].x));
                    }
                    
                    uiController.setTime(times[time]);
                    time += timeScale;       
                }
                else
                    time = 0;

                
            }            
            yield return new WaitForSeconds((float) 0.001/timeScale);
        }   
    }

#endregion

}

