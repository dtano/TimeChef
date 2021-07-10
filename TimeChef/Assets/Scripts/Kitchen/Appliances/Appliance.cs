using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Appliance : MonoBehaviour
{
    protected Item interactedItem;
    public float processingTime;
    protected bool isProcessing = false;
    protected bool isHolding = false;

    protected Animator animator;

    // Allows the appliance to be affected by the player's time abilities
    protected ITimeEffect timeEffect;
    protected AudioSource soundEffect;
    public int timeCost;


    // Update is called once per frame
    protected virtual void Update()
    {
        if(isProcessing){
            //Start a timer here 
        }
    }

    protected abstract void Action();
    protected abstract bool WillAcceptItem(Item givenItem);
    protected abstract void HandleItem(Item givenItem);

    public void UseAppliance(ItemSystem agentItems)
    {
        Debug.Log("Call use appliance");
        if(agentItems.isCarrying()){
                Debug.Log("Player can use this appliance");
                // Means that the player is able to use this appliance
                if(WillAcceptItem(agentItems.GetCurrItem())){
                    // Take the item
                    Debug.Log("Item accepted");
                    AcceptItem(agentItems);
                }else{
                    Debug.Log("Item declined");
                    DeclineItem();
                }
                
        }else{
            Debug.Log("Can't use this appliance");
        }
    }

    protected virtual void AcceptItem(ItemSystem agentItems)
    {
        HandleItem(agentItems.GetCurrItem());
        agentItems.DropItem();
    }

    void DeclineItem()
    {
        Debug.Log("Can't take this item");
    }

    public bool isBusy()
    {
        return isProcessing;
    }

    // Both functions in the bottom are exactly the same as the code in Item, so this could be refactored
    public virtual bool AbleToManipulate()
    {
        return timeEffect != null;
    }

    // Triggers the time manipulation event for an item that has a time effect instance
    public void ManipulateTime(TimeManipulator manipulator)
    {
        if(AbleToManipulate()){
            timeEffect.Effect(manipulator);
        }
    }

    public virtual bool IsManipulated()
    {
        // Some appliances can't be affected
        return false;
    }
}
