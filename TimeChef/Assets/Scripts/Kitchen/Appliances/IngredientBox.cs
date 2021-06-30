using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject ingredientPrefab;

    // This boxSign is the sprite of the ingredient that it instantiates
    public Sprite boxSign;

    // The chance that the box might produce a spoiled ingredient
    public float spoiledChance;
    
    // Start is called before the first frame update
    void Start()
    {
        boxSign = ingredientPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    // Instantiates the specified prefab
    public void Produce(ItemSystem agentItems)
    {
        // Before everything, we need to check whether the player is holding anything or not
        if(agentItems.GetCurrItem() == null){
            // Might need to add an offset to transform.position
            GameObject newIngredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity);

            // Do some chance check here

            // if(agentItems.GetItem(newIngredient)){
            //     Debug.Log("Successfully placed spawned ingredient in the hands of the player");
            // }
            agentItems.ForcePickUp(newIngredient);
            
        }

        
    }

}
