using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float duration;
    private float currTime;
    private bool timeOver = false;
    private bool timerOn = false;

    public Slider timeSlider;
    public Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        timerOn = false;    
    }

    // Update is called once per frame
    void Update()
    {
        // Move the time slider based on where the tool is
        timeSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        
        if(timerOn){
            currTime += Time.deltaTime;

            if(Mathf.Round(currTime) >= duration){
                Debug.Log("Time's up, food's done cooking");
                timeOver = true;
                timeSlider.value = duration;
                Debug.Log(currTime);
                // Need to do some sort of check here for if the food has reached burning threshold
                //timerOn = false;
                //currTime = duration;
            }else{
                Debug.Log(Mathf.Round(currTime));
                timeSlider.value = currTime;
            }
        }
    }

    public void SetDuration(float duration, bool onHold)
    {
        this.duration = duration;
        timeSlider.maxValue = duration;
        if(!onHold){
            //Debug.Log("Pan was not on hold");
            currTime = 0;
            timeSlider.value = duration;
        }else{
            Debug.Log("Pan was on hold");
        }
        // currTime = duration;
        // timeSlider.value = duration;
    }

    public void Activate()
    {
        timerOn = true;
        ShowTimer();
    }

    public void Deactivate()
    {
        HideTimer();
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

    public float GetCurrTime()
    {
        return currTime;
    }

    public void Reset()
    {
        currTime = 0;
        timerOn = false;
        timeOver = false;
    }

    public void HideTimer()
    {
        timeSlider.gameObject.SetActive(false);
    }

    public void ShowTimer()
    {
        timeSlider.gameObject.SetActive(true);
    }
}
