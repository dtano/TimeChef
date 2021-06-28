using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float duration;
    private float currTime;
    private bool stopTimer;
    private bool timerOn = false;

    
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn){
            currTime -= Time.deltaTime;

            if(Mathf.Round(currTime) <= 0){
                Debug.Log("Time's up, food's done cooking");
                stopTimer = true;
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
}
