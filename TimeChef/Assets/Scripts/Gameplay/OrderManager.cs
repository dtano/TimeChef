using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    //  For the plates, maybe I should just send back dirty plates immediately
    //  after an order suite is finished (depending on if there are any failed orders)
    //  A kitchen can only have 4 plates at a time
    private Queue<Order> orders;
    public int numTotalOrders;
    
    // Used to make sure that only 4 plates are in the game at a time
    public int maxPlates = 4;
    
    public GameObject orderPrefab;
    public Transform orderHolder;
    private int numCompletedOrders;

    // This refers to the number of failed orders in an order suite
    //private int suiteFailedOrders;
    // This refers to the number of successful orders in an order suite
    private int suiteSubmittedOrders;

    private Order currOrder;
    private List<Order> orderSuite;
    private PlateManager plateManager;

    //private string[] possibleDishes;

    // Used to determin order number
    private int orderCounter = 1;

    private int totalScore = 0;

    // How long the player has to serve as many orders as possible
    public float operationTime;
    // How many orders a player can serve at a time
    public int maxOrderSuiteLength;

    // This is where the player can serve their dishes
    public OrderWindow orderWindow;
    private System.Random rand;

    public bool startOperation = false;
    private bool inProgress = false;
    private bool completeOperation = false;

    private int score;
    // Represents the player's time manipulation abilities
    private TimeManipulator timeManipulator;

    // A UI element that visually represents the score
    private ScoreController scoreController;
    // A UI element that visually tells the player how far they are to the last order
    private GameProgressController progressController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        orderSuite = new List<Order>();
        plateManager = GameObject.FindGameObjectWithTag("PlateManager").GetComponent<PlateManager>();
        timeManipulator = GameObject.FindGameObjectWithTag("Agent").GetComponent<TimeManipulator>();
        
        InitUI();
        
    }

    // This method is to initialize all UI variables that depend on OrderManager. Only called once
    void InitUI()
    {
        GameObject uiController = GameObject.FindGameObjectWithTag("UIController");
        scoreController = uiController.GetComponent<ScoreController>();
        progressController = uiController.GetComponent<GameProgressController>();
        
        // Initialize the max value to be the max number of orders
        progressController.SetMaxValue(numTotalOrders);
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
            CheckFailedOrders();
        }
    }

    // Generates order objects
    IEnumerator GenerateOrders()
    {
        // Keep making new orders until completed orders is equal to total orders
        while(numCompletedOrders < numTotalOrders){
            int upperBound = maxOrderSuiteLength;
            // Need to make sure numOrders does not exceed the max number of orders that are gonna come out
            if(numCompletedOrders + upperBound > numTotalOrders){
                while(numCompletedOrders + upperBound > numTotalOrders || upperBound > 1){
                    upperBound--;
                }
            }
            
            int numOrders = rand.Next(1,upperBound + 1);
            for(int i = 0; i < numOrders; i++){
                MakeNewOrder();
            }
            //MakeNewOrder();
            // Check whether order suite is completed or not
            // while(currOrder != null){
            //     yield return null;
            // }
            while(orderSuite.Count > 0){
                yield return null;
            }
            
            orderSuite.Clear();
            // Return dirty plates
            ManagePlates();
        }

        ManagePlates();

    }

    void MakeNewOrder()
    {
        Debug.Log("Make new order");
        // Pick a random dish name from the available dishes
        string[] possibleDishes = new string[RecipeBook._instance.recipes.Keys.Count];
        RecipeBook._instance.recipes.Keys.CopyTo(possibleDishes, 0);

        string dishName = possibleDishes[rand.Next(0, possibleDishes.Length)];
        
        // Pick a random customer type. This will impact the longest a customer will wait for their dish
        //CustomerDB.CustomerType customerType = CustomerDB._instance.GetRandomCustType();
        CustomerDB.CustomerType customerType = CustomerDB._instance.GetRandomCustType();
        Debug.Log("Customer " + customerType);
        float waitTime = CustomerDB._instance.waitTimes[customerType];

        GameObject orderObject = InstantiateOrderObject();

        Order order = orderObject.GetComponent<Order>();
        order.MakeOrder(dishName, waitTime, orderCounter);
        orderCounter++;

        currOrder = order;
        orderSuite.Add(order);


    }

    // Call on the plate manager to return the specified amount of plates
    void ManagePlates()
    {
        plateManager.ReturnDirtyDishes(suiteSubmittedOrders);
        suiteSubmittedOrders = 0;
    }

    void CheckFailedOrders()
    {
        // This means that nothing was submitted
        // if(currOrder != null && currOrder.FailedToServe()){
        //     Debug.Log("Remove unfinished order");
        //     numCompletedOrders+=1;
        //     currOrder.EndOrder(false);
        // }

        if(orderSuite.Count > 0){
            // Keeps track of how many points have been lost to failed orders for this suite
            int totalScoreLost = 0;
            foreach(Order order in orderSuite){
                if(order != null && order.FailedToServe()){
                    Debug.Log("Remove unfinished order");
                    numCompletedOrders+=1;
                    order.EndOrder(false);
                    progressController.UpdateValue();
                    score -= 10;
                    totalScoreLost -= 10;
                }
            }
            // Update score change with the total lost score
            if(totalScoreLost < 0){
                scoreController.UpdateScore(totalScoreLost);
            }
        }

        orderSuite.RemoveAll(order => order == null);
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
        // If there are orders with the same dish, pick the order with the least amount of time left
        bool foundMatch = false;

        // Find orders that match this dish
        List<Order> accurateOrders = FindMatchingOrders(submittedDishName);

        // If there is at least one matching order, then it means a match has been found
        if(accurateOrders.Count > 0){
            foundMatch = true;
            accurateOrders[0].EndOrder(true);
            score += 10;
            scoreController.UpdateScore(10);
            timeManipulator.AddPoints(1);
            progressController.UpdateValue();
        }

        // Remove all ended orders
        orderSuite.RemoveAll(order => order == null);

        // If no match was found, then deem the first order a failure
        if(!foundMatch && orderSuite.Count > 0){
            orderSuite[0].EndOrder(false);
            orderSuite.RemoveAt(0);
            score -= 10;
            scoreController.UpdateScore(-10);
            progressController.UpdateValue();
        }


        numCompletedOrders++;
        suiteSubmittedOrders++;
    }

    // Returns a list of matching orders from the order suite
    List<Order> FindMatchingOrders(string dishName)
    {
        // Find orders that match this dish
        List<Order> accurateOrders = new List<Order>();
        orderSuite.ForEach((order) => {
            if(order.CheckDishAccuracy(dishName)){ 
                accurateOrders.Add(order);
            }
        });

        // Then sort by wait time if there is more than one match
        accurateOrders.Sort((a, b) => (int) (b.GetWaitTime() - a.GetWaitTime()));
        return accurateOrders;
    }
    
    public bool HasOrders()
    {
        //return currOrder != null;
        return orderSuite.Count > 0;
    }
}
