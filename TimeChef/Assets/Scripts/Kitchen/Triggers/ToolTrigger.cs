using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTrigger : InteractionTrigger
{
    Kitchenware tool;
    // Text that appears when the player is in the trigger zone of a tool that can be sped up
    public TMPro.TextMeshProUGUI speedUpText;
    
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        tool = GetComponentInParent<Kitchenware>();
        if(speedUpText != null){
            speedUpText.text = "Speed Up";
            speedUpText.enabled = false;
        }
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
                Debug.Log("SPACE ON TOOL!");
                if(agentItems.GetItem(transform.parent.gameObject)){
                    Deactivate();
                }
            }

            if(Input.GetKeyDown(KeyCode.F)){
                Debug.Log("Attempt to speed up process");
                if(tool.AbleToManipulate()){
                    TimeManipulator manipulator = gameplayAgent.GetComponent<TimeManipulator>();
                    // Hide speed up text and show the hourglass sprite
                    if(speedUpText != null){
                        speedUpText.enabled = false;
                    }
                    tool.ManipulateTime(manipulator);
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
            if(!tool.isFullCapacity() && !tool.IsHoldingACookedItem()){
                tool.AddIngredient((Ingredient) carriedItem);
                // Then delete the gameObject
                agentItems.Dispose();
            }

        }else if(carriedItem is Plate){
            // Only allow to transfer to dish if the food in the pan is at least cooked
            if(tool.IsHoldingACookedItem()){
                // Then transfer the cooked food to the dish
                tool.TransferContents((Plate) carriedItem);
            }
        }
    }

    // EnterEffect is probably a better name
    protected override void TriggerEffect()
    {
        if(tool.IsCooking() && !tool.IsHoldingACookedItem() && !tool.IsManipulated()){
            Vector3 offset = new Vector3(0, 1f, 0);
            speedUpText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            speedUpText.enabled = true;
        }
    }

    protected override void ExitEffect()
    {
        if(speedUpText != null){
            speedUpText.enabled = false;
        }
    }

}
