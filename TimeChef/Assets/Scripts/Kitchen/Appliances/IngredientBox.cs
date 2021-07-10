using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    public GameObject ingredientPrefab;

    // The chance that the box might produce a spoiled ingredient
    public float spoiledChance;
    private int maxValue;
    private Animator anim;
    System.Random rand;

    void Start()
    {
        rand = new System.Random();
        maxValue = 1;
        anim = GetComponent<Animator>();
    }

    // Instantiates the specified prefab
    public void Produce(ItemSystem agentItems)
    {
        // Before everything, we need to check whether the player is holding anything or not
        if(agentItems.GetCurrItem() == null){
            // Might need to add an offset to transform.position
            GameObject newIngredient = InstantiateIngredient();

            // Do some chance check here
            if(rand.Next(maxValue) < spoiledChance){
                Debug.Log("Spoiled ingredient!");
            }

            // if(agentItems.GetItem(newIngredient)){
            //     Debug.Log("Successfully placed spawned ingredient in the hands of the player");
            // }
            agentItems.ForcePickUp(newIngredient);
            Animate();
            
        }else{
            // Then we need to check what kind of item the player is holding
            if(agentItems.GetCurrItem() is Plate){
                // Then we need to see if we can add the ingredient to the plate
                Plate carriedPlate = (Plate) agentItems.GetCurrItem();
                if(!carriedPlate.IsFull()){
                    GameObject newIngredient = InstantiateIngredient();
                    carriedPlate.AddIngredient(newIngredient.GetComponent<Ingredient>());

                    // Because we only need the ingredient script
                    Destroy(newIngredient);
                    Animate();
                }
            }
        }

        
    }

    GameObject InstantiateIngredient()
    {
        return Instantiate(ingredientPrefab, transform.position, Quaternion.identity);
    }

    void Animate()
    {
        anim.SetTrigger("Open");
    }

}
