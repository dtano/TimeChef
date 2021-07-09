using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject ingredientPrefab;

    // The chance that the box might produce a spoiled ingredient
    public float spoiledChance;
    private int maxValue;
    System.Random rand;

    void Start()
    {
        rand = new System.Random();
        maxValue = 1;
    }

    // Instantiates the specified prefab
    public void Produce(ItemSystem agentItems)
    {
        // Before everything, we need to check whether the player is holding anything or not
        if(agentItems.GetCurrItem() == null){
            // Might need to add an offset to transform.position
            GameObject newIngredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity);

            // Do some chance check here
            if(rand.Next(maxValue) < spoiledChance){
                Debug.Log("Spoiled ingredient!");
            }

            // if(agentItems.GetItem(newIngredient)){
            //     Debug.Log("Successfully placed spawned ingredient in the hands of the player");
            // }
            agentItems.ForcePickUp(newIngredient);
            
        }

        
    }

}
