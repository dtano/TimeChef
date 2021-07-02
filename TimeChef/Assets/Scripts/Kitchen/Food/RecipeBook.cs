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

    public Dictionary<string, Sprite> dishSprites;
    //public Dictionary<string, string> dishSprites;
    
    // // Dishnames and ingredientsNeeded need to have the same length
    // public string[] dishNames;
    // // Ingredients will be combined with each other into one string. e.g., pan burger, chopped lettuce, chopped tomato, bun
    // public string[] ingredientsNeeded;
    // public struct DishSprites {
    //     public string name;
    //     public Sprite sprite;
    // }
    // public DishSprites[] dishSprites;

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
            {"Onion Soup", new string[] {"onion", "onion", "onion"}},
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
            {"onion", new string[] {"Onion Soup"}},
            {"chopped onion", new string[] {"Potato Salad"}},
            {"tomato", new string[] {"Tomato Soup"}},
            {"chopped tomato", new string[]{"Garden Salad"}},
            {"lettuce", new string[]{"Garden Salad"}}
        };

        // Gonna have to do a resources load
        dishSprites = new Dictionary<string, Sprite>(){
            {"Onion Soup", Resources.Load<Sprite>("Sprites/Dishes/OnionSoup")},
            {"Garden Salad", Resources.Load<Sprite>("Sprites/Dishes/GardenSalad")},
            {"Potato Salad", Resources.Load<Sprite>("Sprites/Dishes/PotatoSalad")},
            {"Tomato Soup", Resources.Load<Sprite>("Sprites/Dishes/TomatoSoup")},
            {"Muck", Resources.Load<Sprite>("Sprites/Dishes/Muck")}
        };

        
    }
}
