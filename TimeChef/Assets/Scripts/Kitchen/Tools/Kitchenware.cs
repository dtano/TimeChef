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
    // The maximum time after food is cooked before it burns
    public float burnThreshold;
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

        // Instantiate a TimeAccelarator, since we want the player to be able to speed up cooking processes
        //timeEffect = new TimeAccelerator(timer);

        // Make sure the cooking timer is not visible at the start
        timer.Deactivate();
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

        // Checks that are done when the kitchenware is in the process of cooking
        if(isCooking){
            // Then food is finished
            if(timer.IsTimerFinished()){
                // Now keep track of how long the food is left to cook after its cooking time
                //BurnCheck();
                timer.Stop();
                isCooking = false;
            }
        }


    }

    // Add an ingredient to the list
    public void AddIngredient(Ingredient ingredient)
    {
        // Change sprite here
        if(ingredients.Count < numAcceptedIngredients){
            ingredients.Add(ingredient);
            HandleSpriteChange(ingredient);
            if(isCooking){
                // Reset the counter back to 0
                timer.ResetTime();
            }
            
        }
    }

    void HandleSpriteChange(Ingredient ingredient)
    {
        string[] subs = ingredient.ingredientName.Split(' ');
        string ingTrueName = subs[0];
        if(subs.Length > 1){
            ingTrueName = subs[1];
        }
        Sprite newSprite = Resources.Load<Sprite>($"Sprites/PotSprites/{ingTrueName}Pot");
        spriteRenderer.sprite = newSprite;
    }

    // Combination of ingredients on the pan/pot to produce a new ingredient
    // This might be a function for a plate object instead of putting it in pan
    public string IngredientSynthesis()
    {
        foreach(Ingredient ing in ingredients){
            ing.Cook();
        }
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
                    return entry.Key;
                }
            }

        }

        
        Debug.Log("No recipe with this combo");
        return null;
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

    // Checks whether the food in the pan or pot is burnt
    // public void BurnCheck()
    // {
    //     // Count the seconds the food is in the pan after its supposed to be taken
    //     float currTime = timer.GetCurrTime();
    //     if(currTime > cookingTime + burnThreshold){
    //         foreach(Ingredient ing in ingredients){
    //             ing.Overcook();
    //         }
    //         Debug.Log("Food is burnt");

    //         // Force the timer to finish
    //         timer.Deactivate();
    //         // Show some burnt UI on top of the item
    //     }
    // }

    // Place the pan on an appliance
    public void PlaceOnAppliance(Appliance appliance)
    {
        isOnAppliance = true;
        cookingTime = appliance.processingTime;
        //timeEffect.SetCost(appliance.timeCost);
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

    public bool IsEmpty()
    {
        return ingredients.Count == 0;
    }

    public override void Reset()
    {
        ingredients.Clear();
        onHold = false;
        isCooking = false;

        // Reset the timer
        timer.FullReset();

        // Reset sprite too
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/PotSprites/Pot");
        
    }

    // Moves any cooked item from the pan to a dish
    public void TransferContents(Plate plate)
    {
        //plate.AddMultipleIngredients(ingredients);
        // Have to check whether kitchenware was able to synthesize a new dish or not
        
        if(!plate.IsPlateDirty() && ingredients.Count > 0){
            string dishName = IngredientSynthesis();
            if(dishName != null){
                Debug.Log("Dish in kitchenware successfully transferred to plate");
                plate.SetDish(dishName);
            }else{
                if(ingredients.Count == numAcceptedIngredients){
                    Debug.Log("Failed combination while cooking");
                    plate.SetDish("muck");
                }else{
                    // Means we gotta transfer ingredients
                    Debug.Log("Transfer remaining ingredients to plate");
                    plate.AddMultipleIngredients(ingredients);
                }
            }

            Debug.Log("Food moved to dish");
            Reset();
        }
    }

    public override bool AbleToManipulate()
    {
        if(timeEffect != null && IsCooking()){
            Debug.Log("Able to manipulate pot");
            return true;
        }
        Debug.Log("Can't manipulate pot");
        return false;
    }

    // A check to see whether the player has affected this item with time manipulation
    public bool IsManipulated()
    {
        if(timer.GetTimeMultiplier() != 1){
            return true;
        }
        return false;
    }

    public Timer GetTimer()
    {
        return timer;
    }

}
