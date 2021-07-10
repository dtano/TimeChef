using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimer : Timer
{
    public TMPro.TextMeshProUGUI valueText;
    // Update is called once per frame
    protected override void Update()
    {
        if(timerOn){
            currTime -= Time.deltaTime;

            if(Mathf.Round(currTime) <= 0){
                Debug.Log("Time's up");
                timeOver = true;
                timeSlider.value = 0;
                //Debug.Log(currTime);
            }else{
                //Debug.Log(Mathf.Round(currTime));
                timeSlider.value = currTime;
                DisplayTimerValue();
            }
        }
    }

    public override void SetDuration(float duration, bool onHold)
    {
        this.duration = duration;
        timeSlider.maxValue = duration;
        if(!onHold){
            //Debug.Log("Pan was not on hold");
            currTime = duration;
            timeSlider.value = currTime;
        }
    }

    void DisplayTimerValue()
    {
        valueText.text = Mathf.Round(currTime).ToString();
    }
}
