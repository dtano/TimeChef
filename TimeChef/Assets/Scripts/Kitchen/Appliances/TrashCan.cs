using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Appliance
{
    void Start()
    {
        animator = GetComponent<Animator>();
        soundEffect = GetComponent<AudioSource>();
    }
    
    protected override bool WillAcceptItem(Item givenItem)
    {
        // Can accept ingredients (fresh, whole, burnt, spoiled)
        // Kitchenware containing food, plate containing food. I think item is fine
        if(givenItem is Ingredient){
            return true;
        }else if(givenItem is Kitchenware){
            // Check whether it has anything in it
            Kitchenware tool = (Kitchenware) givenItem;
            if(!tool.IsEmpty()){
                return true;
            }
        }else if(givenItem is Plate){
            Plate plate = (Plate) givenItem;
            if(!plate.IsEmpty()){
                return true;
            }
        }
        return false;
        // if(givenItem is Ingredient || givenItem is Kitchenware || givenItem is Plate){
        //     return true;
        // }

        // return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        animator.SetTrigger("Operate");
        soundEffect.Play();
        givenItem.Reset();
        // Play trash sound effect
        
    }

    protected override void AcceptItem(ItemSystem agentItems)
    {
        if(agentItems.GetCurrItem() is Ingredient){
            HandleItem(agentItems.GetCurrItem());
            agentItems.DropItem();
        }else{
            HandleItem(agentItems.GetCurrItem());
        }
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
}
