using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchenware : Item
{
    // Accepts ingredients
    private List<Ingredient> ingredients;
    // Start is called before the first frame update
    void Start()
    {
        // Takes in one or more ingredients
        ingredients = new List<Ingredient>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

}
