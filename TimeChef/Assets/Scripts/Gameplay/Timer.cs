using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float duration;
    private float currTime;
    private bool timeOver = false;
    private bool timerOn = false;

    
    // Start is called before the first frame update
    void Start()
    {
        timerOn = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn){
            currTime -= Time.deltaTime;

            if(Mathf.Round(currTime) <= 0){
                Debug.Log("Time's up, food's done cooking");
                timeOver = true;
                timerOn = false;
                currTime = duration;
            }else{
                Debug.Log(Mathf.Round(currTime));
            }
        }
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
        currTime = duration;
    }

    public void Activate()
    {
        timerOn = true;
    }

    public void Deactivate()
    {
        timerOn = false;
    }

    public bool IsTimerOn()
    {
        return timerOn;
    }

    public bool IsTimerFinished()
    {
        return timeOver;
    }

    public void Reset()
    {
        currTime = duration;
        //timerOn = false;
        timeOver = false;
    }
}
