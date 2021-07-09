using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    // What does an order consist of?
    public int orderNum;
    public string dishName;
    private Timer timer;
    private float waitTime;

    public TMPro.TextMeshProUGUI dishText;
    public Image dishImage;

    private bool inProgress = false;
    private bool isOrderInitialized = false;
    private bool isFailed = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.IsTimerFinished()){
            Debug.Log("Player failed to serve on time");
            isFailed = true;
            //EndOrder(false);
        }
        
        if(!inProgress && isOrderInitialized){
            timer.SetDuration(waitTime, false);
            timer.Activate();
            inProgress = true;
        }
    }

    public void MakeOrder(string dishName, float waitTime, int orderNum)
    {
        if(!isOrderInitialized){
            // This is to make the order 
            if(RecipeBook._instance.recipes.ContainsKey(dishName)){
                this.dishName = dishName;
                dishText.text = dishName;
                dishImage.sprite = RecipeBook._instance.dishSprites[dishName];
            }

            if(waitTime > 0){
                this.waitTime = waitTime;
            }

            if(orderNum > 0){
                this.orderNum = orderNum;
            }
            isOrderInitialized = true;
        }
    }

    public void EndOrder(bool success)
    {
        if(!success){
            Debug.Log("FAIL TO SERVE ORDER #" + orderNum.ToString());
        }
        float endTime = timer.GetCurrTime();
        Debug.Log("Order finished at " + Mathf.Round(endTime).ToString());
        Destroy(gameObject);
    }

    public bool CheckDishAccuracy(string dishName)
    {
        return this.dishName == dishName;
    }

    public bool FailedToServe()
    {
        return isFailed;
    }

    public float GetWaitTime()
    {
        return waitTime;
    }
}
