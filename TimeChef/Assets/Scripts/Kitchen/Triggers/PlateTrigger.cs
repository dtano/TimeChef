using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : InteractionTrigger
{
    Plate plate;
    protected override void Awake()
    {
        plate = GetComponentInParent<Plate>();
    }

    // This interact is actually similar to ToolTrigger, so this could be refactored
    protected override void Interact()
    {
        if(agentItems.isCarrying()){
            if(Input.GetKeyDown(KeyCode.E)){
                HandleInteractions();

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

    void HandleInteractions()
    {
        Item carriedItem = agentItems.GetCurrItem();
        
        // You can only add things to a clean plate
        if(!plate.IsPlateDirty()){
            if(carriedItem is Ingredient){
                // Check the ingredient status? Or maybe just add it anyway
                if(((Ingredient) carriedItem).IsSpoiled() || ((Ingredient) carriedItem).IsBurnt()){
                    Debug.Log("Can't accept spoiled or burned ingredient");
                }else{
                    if(plate.AddIngredient((Ingredient) carriedItem)){
                        agentItems.Dispose();
                    }else{
                        Debug.Log("Can't accept this ingredient, because plate is full");
                    }
                }
            }else if(carriedItem is Kitchenware){
                // Check whether the pan is done cooking
                if(((Kitchenware) carriedItem).IsHoldingACookedItem()){
                    // Then we could transfer the contents of the pan here
                    ((Kitchenware) carriedItem).TransferContents(plate);
                }
            }
        }
    }

    protected override void TriggerEffect()
    {
    }

    protected override void ExitEffect()
    {
    }
}
