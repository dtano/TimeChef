using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressUI : MonoBehaviour
{
    private Slider progressSlider;

    void Awake()
    {
        progressSlider = transform.GetChild(0).GetComponent<Slider>();
        //Debug.Log(progressSlider);
        progressSlider.value = 0;    
    }
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     progressSlider = transform.GetChild(0).GetComponent<Slider>();
    //     //Debug.Log(progressSlider);
    //     progressSlider.value = 0;    
    // }

    // In this game, progress is determined by how many orders have been given
    public void SetMaxValue(int maxOrders)
    {
        progressSlider.maxValue = maxOrders;
    }

    public void UpdateValue(int numOrders)
    {
        if(progressSlider.value + numOrders > progressSlider.maxValue){
            progressSlider.value = progressSlider.maxValue;
        }else{
            progressSlider.value = progressSlider.value + numOrders;
        }
    }

    public bool GameFinished()
    {
        return progressSlider.value == progressSlider.maxValue;
    }
}
