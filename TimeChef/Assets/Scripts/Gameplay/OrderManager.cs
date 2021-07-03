using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private Queue<Order> orders;
    public int numTotalOrders;
    public GameObject orderPrefab;
    private int numCompletedOrders;

    private string[] possibleDishes;

    // Used to determin order number
    private int orderCounter = 0;

    private int totalScore = 0;

    // How long the player has to serve as many orders as possible
    public float operationTime;
    
    // This is where the player can serve their dishes
    public OrderWindow orderWindow;
    private System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        possibleDishes = new string[RecipeBook._instance.recipes.Keys.Count];
        RecipeBook._instance.recipes.Keys.CopyTo(possibleDishes, 0);
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeNewOrder()
    {
        // Pick a random dish name from the available dishes
        string dishName = possibleDishes[rand.Next(0, possibleDishes.Length)];
        // Pick a random customer type. This will impact the longest a customer will wait for their dish
        float waitTime = 20;
        
        GameObject orderObject = Instantiate(orderPrefab, transform.position, Quaternion.identity);
        Order order = orderObject.GetComponent<Order>();
        order.MakeOrder(dishName, waitTime, orderCounter);
        orderCounter++;
    }

    void FinishOrder()
    {

    }
}
