using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTrigger : InteractionTrigger
{
    Kitchenware tool;
    // Start is called before the first frame update
    protected override void Awake()
    {
        tool = GetComponentInParent<Kitchenware>();
    }

    protected override void Interact()
    {
        if(agentItems.isCarrying()){
            if(Input.GetKeyDown(KeyCode.E)){
                HandleToolInteractions();

            }
        }else{
            // If the player isn't carrying anything
            if(Input.GetKeyDown(KeyCode.Space)){
                if(agentItems.GetItem(transform.parent.gameObject)){
                    Deactivate();
                }
            }
        }
    }

    // This function handles cases where the player is carrying another item and wants to interact with this tool
    void HandleToolInteractions()
    {
        Item carriedItem = agentItems.GetCurrItem();
        if(carriedItem is Ingredient){
            // Add ingredient to the pan/pot
            if(!tool.isFullCapacity()){
                tool.AddIngredient((Ingredient) carriedItem);
                // Then delete the gameObject
                agentItems.Dispose();
            }

        }else if(carriedItem is Kitchenware){

        }
    }
}
