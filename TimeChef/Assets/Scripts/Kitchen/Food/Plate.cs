using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lets assume this is a plate
public class Plate : Item
{
    // Plates act as a holder of ingredients
    // It can hold a limited amount of ingredients
    // Changes sprite based on what dish was created using the given ingredients
    // How does it know which sprite to change to???
    
    // Signifies whether a proper dish is being carried by this dish
    private bool holdCompleteDish = false;
    private bool isFailed = false;
    
    private bool isDirty = false;
    private int maxContents = 3;
    private List<Ingredient> ingredients;

    public Sprite dirtySprite;
    public Sprite cleanSprite;

    // This can be filled when a complete dish is either tranferred here or created
    private string dishName;

    // Dish and kitchenware can both synthesize ingredients to food, so an interface might be appropriate

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<Ingredient>();

        if(isDirty){
            spriteRenderer.sprite = dirtySprite;
        }else{
            spriteRenderer.sprite = cleanSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!holdCompleteDish && ingredients.Count > 0){
            Debug.Log(ingredients.Count);
        }
    }

    // This would be the same as the one from kitchenware
    void IngredientSynthesis()
    {
        // Combine ingredients and find its resulting dish
        // If it doesn't exist, then it will turn into muck
        List<string> ingredientNames = new List<string>();
        
        // Check whether any of the ingredients are burnt
        foreach(Ingredient ing in ingredients){
            if(ing.IsBurnt()){
                // This means no matter what the dish will be invalid
                Debug.Log("Created muck");

                PlateFailedDish();
                holdCompleteDish = true;
                return;
            }
            ingredientNames.Add(ing.ingredientName);
        }

        // Check for proper ingredient combinations here
        // Look thorough all the recipes and find the one that is equal
        foreach(KeyValuePair<string, string[]> entry in RecipeBook._instance.recipes){
            if(ingredients.Count == entry.Value.Length){
                List<string> recipeIng = new List<string>(entry.Value);
                var isEqual = new HashSet<string>(recipeIng).SetEquals(ingredientNames);
                if(isEqual){
                    // Found a recipe for this combination
                    Debug.Log("Found a recipe");
                    SetDish(entry.Key);
                    return;
                }
            }

        }

        // If we reach this part and the plate is holding 3 ingredients already,
        // it means that no matter what, the current combination is a bust
        if(ingredients.Count == 3){
            PlateFailedDish();
            holdCompleteDish = true;
            
        }
    }

    public bool AddIngredient(Ingredient ingredient)
    {
        if(!IsFull()){
            ingredients.Add(ingredient);
            // Everytime you add an ingredient, try to see if its a proper combination
            IngredientSynthesis();
            return true;
        }
        return false;
    }

    public void AddMultipleIngredients(List<Ingredient> newIngredients)
    {
        if(newIngredients.Count + newIngredients.Count <= maxContents && !holdCompleteDish){
            ingredients.AddRange(newIngredients);
            IngredientSynthesis();
        }
    }

    // Sets the dish to the given name
    public void SetDish(string dishName)
    {
        if(RecipeBook._instance.recipes.ContainsKey(dishName)){
            // Then its a valid dish
            this.dishName = dishName;
            Debug.Log("Valid dish plated");
            spriteRenderer.sprite = RecipeBook._instance.dishSprites[dishName];
            
        }else{
            // Means that its muck
            Debug.Log("You plated muck");
            PlateFailedDish();
        }
        // What if the plate has some ingredients on it? Just override the ingredients
        ingredients.Clear();
        holdCompleteDish = true;
        
    }

    void PlateFailedDish()
    {
        this.dishName = "Muck";
        spriteRenderer.sprite = RecipeBook._instance.dishSprites[dishName];
        isFailed = true;
    }

    // Clear ingredients and change sprite back
    public override void Reset()
    {
        ingredients.Clear();
        isFailed = false;
        holdCompleteDish = false;
        dishName = null;
        spriteRenderer.sprite = cleanSprite;
    }

    public bool IsPlateDirty()
    {
        return isDirty;
    }

    public bool IsFull()
    {
        return ingredients.Count == maxContents || holdCompleteDish;
    }

    public bool IsEmpty()
    {
        if(ingredients.Count == 0 && !holdCompleteDish){
            // Means that its empty
            return true;
        }
        return false;
    }

    // Dishwasher will call this function to change the plate's dirty status
    public void Wash()
    {
        isDirty = false;
        spriteRenderer.sprite = cleanSprite;
    }

    public void MakeDirty()
    {
        isDirty = true;
        spriteRenderer.sprite = dirtySprite;
    }

    public bool IsHoldingDish()
    {
        return holdCompleteDish;
    }

    public string GetDishName()
    {
        return dishName;
    }



}
