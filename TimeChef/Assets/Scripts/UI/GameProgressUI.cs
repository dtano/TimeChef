using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressUI : MonoBehaviour
{
    private Slider progressSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        progressSlider = transform.GetChild(0).GetComponent<Slider>();
        progressSlider.value = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
