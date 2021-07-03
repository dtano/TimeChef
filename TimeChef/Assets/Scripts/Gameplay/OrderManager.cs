using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private Queue<Order> orders;
    public int numTotalOrders;
    public GameObject orderPrefab;
    public Transform orderHolder;
    private int numCompletedOrders;

    private Order currOrder;

    //private string[] possibleDishes;

    // Used to determin order number
    private int orderCounter = 1;

    private int totalScore = 0;

    // How long the player has to serve as many orders as possible
    public float operationTime;
    
    // This is where the player can serve their dishes
    public OrderWindow orderWindow;
    private System.Random rand;

    public bool startOperation = false;
    private bool inProgress = false;
    private bool completeOperation = false;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

        if(!completeOperation && startOperation){
            // MakeNewOrder();
            StartCoroutine(GenerateOrders());
            startOperation = false;
            inProgress = true;
            // Start the order process
            // Only make a new order when an order suite is completed
        }

        if(!completeOperation && inProgress){
            if(numCompletedOrders == numTotalOrders){
                completeOperation = true;
                inProgress = false;
                Debug.Log("All orders have been given");
                
                // Clear any remaining orders
                // if(currOrder != null){
                //     currOrder.EndOrder(false);
                // }
            }

            // Check orders here
            CheckOrders();
        }
    }

    // Generates order objects
    IEnumerator GenerateOrders()
    {
        // Keep making new orders until completed orders is equal to total orders
        while(numCompletedOrders < numTotalOrders){
            MakeNewOrder();
            // Check whether order suite is completed or not
            while(currOrder != null){
                yield return null;
            }
        }

    }

    void MakeNewOrder()
    {
        Debug.Log("Make new order");
        // Pick a random dish name from the available dishes
        string[] possibleDishes = new string[RecipeBook._instance.recipes.Keys.Count];
        RecipeBook._instance.recipes.Keys.CopyTo(possibleDishes, 0);
        
        string dishName = possibleDishes[rand.Next(0, possibleDishes.Length)];
        // Pick a random customer type. This will impact the longest a customer will wait for their dish
        float waitTime = 20;
        
        GameObject orderObject = InstantiateOrderObject();
        
        Order order = orderObject.GetComponent<Order>();
        order.MakeOrder(dishName, waitTime, orderCounter);
        orderCounter++;

        currOrder = order;


    }

    void CheckOrders()
    {
        // This means that nothing was submitted
        if(currOrder != null && currOrder.FailedToServe()){
            Debug.Log("Remove unfinished order");
            numCompletedOrders+=1;
            currOrder.EndOrder(false);
        }
    }

    GameObject InstantiateOrderObject()
    {
        // Now we need to loop through the children of order holder to find an empty slot
        Transform availableOrderSlot = null;
        for(int i = 0; i < orderHolder.transform.childCount; i++){
            if(orderHolder.transform.GetChild(i).childCount == 0){
                availableOrderSlot = orderHolder.transform.GetChild(i).transform;
                break;
            }
        }
        GameObject orderObject = Instantiate(orderPrefab, availableOrderSlot.position, Quaternion.identity);
        orderObject.transform.parent = availableOrderSlot;

        return orderObject;
    }

    // Order window will pass this dish to the order manager
    public void SubmitOrder(Plate dish)
    {
        string submittedDishName = dish.GetDishName();
        // if(orders.Peek().CheckDishAccuracy(submittedDishName)){
        //     // Order is correct
        //     orders.Dequeue().EndOrder(true);
        // }
        if(currOrder.CheckDishAccuracy(submittedDishName)){
            currOrder.EndOrder(true);
        }else{
            currOrder.EndOrder(false);
        }
        numCompletedOrders++;
    }

    public bool HasOrders()
    {
        return currOrder != null;
    }
}
