using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchenware : Item
{
    // Accepts ingredients
    private List<Ingredient> ingredients;

    // A list of ingredients that a tool can accept
    public List<string> acceptedIngredients;

    private bool isCooking = false;
    private bool isOnAppliance = false;
    // Status of cooked food that is in it
    private bool onHold = false;
    
    public int numAcceptedIngredients;

    private float cookingTime;
    // This will store the current time at the timer when the pan was removed from the stove
    private float currTime;

    private Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Takes in one or more ingredients
        ingredients = new List<Ingredient>();
        acceptedIngredients = new List<string>();
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

        if(isCooking){
            if(timer.IsTimerFinished()){
                // Then food is finished
                IngredientSynthesis();
            }
        }
    }

    // Add an ingredient to the list
    public void AddIngredient(Ingredient ingredient)
    {
        if(ingredients.Count < numAcceptedIngredients){
            ingredients.Add(ingredient);
            if(isCooking){
                // Reset the counter back to 0
                timer.Reset();
            }
            
        }
    }

    // Combination of ingredients on the pan/pot to produce a new ingredient
    // This might be a function for a plate object instead of putting it in pan
    public void IngredientSynthesis()
    {
        // Get the names of the ingredients
        List<string> ingredientNames = new List<string>();
        foreach(Ingredient ing in ingredients){
            ingredientNames.Add(ing.ingredientName);
        }
        
        // Look thorough all the recipes and find the one that is equal
        foreach(KeyValuePair<string, string[]> entry in RecipeBook._instance.recipes){
            if(ingredients.Count == entry.Value.Length){
                List<string> recipeIng = new List<string>(entry.Value);
                var isEqual = new HashSet<string>(recipeIng).SetEquals(ingredientNames);
                if(isEqual){
                    // Found a recipe for this combination
                    Debug.Log("Found a recipe");
                    return;
                }
            }

        }

        Debug.Log("No recipe with this combo");
        
    }

    // Checks whether the pan is currently placed on an appliance
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
            // Stop the timer since the pan was removed in the middle of cooking
            Debug.Log("Pan was still cooking when it was removed from stove");
            timer.Deactivate();
            currTime = timer.GetCurrTime();
            isCooking = false;
            onHold = true;
        }
        Debug.Log("Tool removed from appliance");
    }

    // Place the pan on an appliance
    public void PlaceOnAppliance(Appliance appliance)
    {
        isOnAppliance = true;
        cookingTime = appliance.processingTime;
        Debug.Log("Tool placed on appliance");

    }

    // Checks whether this item can accept anymore ingredients
    public bool isFullCapacity()
    {
        return ingredients.Count == numAcceptedIngredients;
    }

    // Checks whether the food in the pan is still cooking or not
    public bool IsCooking()
    {
        return isCooking;
    }

    public void InitiateTimer()
    {
        timer.SetDuration(cookingTime, onHold);
        if(onHold){
            onHold = false;
        }
        timer.Activate();
    }

    public bool IsHoldingACookedItem()
    {
        return timer.IsTimerFinished();
    }

}
