using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchenware : Item
{
    // Accepts ingredients
    private List<Ingredient> ingredients;
    private Ingredient[] ing;

    private bool isCooking = false;
    private bool isOnAppliance = false;
    public int numAcceptedIngredients;

    private float cookingTime;
    private float currTime = 0f;

    private Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Takes in one or more ingredients
        ingredients = new List<Ingredient>();
        ing = new Ingredient[3];
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether or not the tool is on the appliance and that the tool is holding more than 1 ingredient
        if(isOnAppliance && !isCooking && ingredients.Count > 0){
            // Then tool is ready to be used to cook
            isCooking = true;
            InitiateTimer();
            //Debug.Log("Initiate timer");
        }

        // if(isCooking && currTime >= cookingTime){
        //     // Ding sound
        //     // Indicate that the dish is done 
        //     // Call a function that creates the dish
        //     Debug.Log("It's time to take the dish");
        // }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        if(isCooking){
            // Reset the counter back to 0
        }else{
            // Start the cooking counter

        }
    }

    public bool isBusy()
    {
        return isCooking;
    }

    public bool OnAppliance()
    {
        return isOnAppliance;
    }

    public void RemoveFromAppliance()
    {
        isOnAppliance = false;
        
        // Means that the pan is either finished cooking or han't started at all
        if(!isCooking){
            cookingTime = 0;
        }else{
            Debug.Log("Pan was still cooking when it was removed from stove");
            timer.Deactivate();
            isCooking = false;
        }
        Debug.Log("Tool removed from appliance");
    }

    public void PlaceOnAppliance(Appliance appliance)
    {
        isOnAppliance = true;
        cookingTime = appliance.processingTime;
        Debug.Log("Tool placed on appliance");

    }

    public bool isFullCapacity()
    {
        return ingredients.Count == numAcceptedIngredients;
    }

    public bool IsCooking()
    {
        return isCooking;
    }

    public void InitiateTimer()
    {
        timer.SetDuration(cookingTime);
        timer.Activate();
    }

}
