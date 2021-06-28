using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public static RecipeBook _instance;
    
    public Dictionary<string, string[]> recipes;
    // A dictionary that records what ingredients are accepted by which cooking tool
    public Dictionary<string, string[]> acceptedIngredients;

    // Maps ingredients to the possible dishes it can create
    public Dictionary<string, string[]> ingredientDishMapping;
    
    // // Dishnames and ingredientsNeeded need to have the same length
    // public string[] dishNames;
    // // Ingredients will be combined with each other into one string. e.g., pan burger, chopped lettuce, chopped tomato, bun
    // public string[] ingredientsNeeded;


    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }else{
            _instance = this;
        }
    }

    void Start()
    {
        recipes = new Dictionary<string, string[]>(){
            {"French Onion Soup", new string[] {"onion", "onion", "onion"}},
            {"Garden Salad", new string[] {"lettuce", "chopped tomato"}},
            {"Potato Salad", new string[] {"boiled potato", "chopped onion", "chopped tomato"}},
            {"Tomato Soup", new string[] {"tomato", "tomato", "tomato"}}
        };
        // for(int i = 0; i < dishNames.Length; i++){
        //     recipes.Add(dishNames[i], ingredientsNeeded[i].Split(','));
        // }
        //kii = new string[2];

        acceptedIngredients = new Dictionary<string, string[]>(){
            {"Pot", new string[] {"tomato", "onion", "potato"}},
            {"Cutting board", new string[] {"lettuce", "tomato", "onion", "potato"}}
        };

        ingredientDishMapping = new Dictionary<string, string[]>(){
            {"chopped potato", new string[] {"Potato Salad"}},
            {"onion", new string[] {"French Onion Soup"}},
            {"chopped onion", new string[] {"Potato Salad"}},
            {"tomato", new string[] {"Tomato Soup"}},
            {"chopped tomato", new string[]{"Garden Salad"}},
            {"lettuce", new string[]{"Garden Salad"}}
        };
    }
}
