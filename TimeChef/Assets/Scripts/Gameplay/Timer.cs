using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    protected float duration;
    protected float currTime;
    protected bool timeOver = false;
    protected bool timerOn = false;

    public Slider timeSlider;
    public Image finishedIndicator;
    public Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        timerOn = false; 
        // if(finishedIndicator != null){
        //     finishedIndicator.enabled = false;
        // }   
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Move the time slider based on where the tool is
        timeSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        
        if(timerOn){
            currTime += Time.deltaTime;

            if(Mathf.Round(currTime) >= duration){
                Debug.Log("Time's up, food's done cooking");
                timeOver = true;
                timeSlider.value = duration;

                if(finishedIndicator != null){
                    IndicateOver();
                }
                // Need to do some sort of check here for if the food has reached burning threshold
                //timerOn = false;
                //currTime = duration;
            }else{
                Debug.Log(Mathf.Round(currTime));
                timeSlider.value = currTime;
            }
        }
    }

    public virtual void SetDuration(float duration, bool onHold)
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
        StartTimer();
        ShowTimer();
    }

    public void Deactivate()
    {
        HideTimer();
        Stop();
    }

    public void StartTimer()
    {
        timerOn = true;
    }

    public void Stop()
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

    public float GetCurrTime()
    {
        return currTime;
    }

    public void FullReset()
    {
        currTime = 0;
        duration = 0;
        timerOn = false;
        timeOver = false;
        HideTimer();
    }

    public void ResetTime()
    {
        currTime = 0;
        if(finishedIndicator != null){
            //finishedIndicator.sprite = null;
            finishedIndicator.enabled = false;
        }
    }

    // Indicate that the process is over
    void IndicateOver()
    {
        //finishedIndicator.sprite = Resources.Load<Sprite>("Sprites/UI/TickMark");
        finishedIndicator.enabled = true;
    }

    public void HideTimer()
    {
        timeSlider.gameObject.SetActive(false);
        if(finishedIndicator != null){
            //finishedIndicator.sprite = null;
            finishedIndicator.enabled = false;
        }
    }

    public void ShowTimer()
    {
        timeSlider.gameObject.SetActive(true);
    }
}
