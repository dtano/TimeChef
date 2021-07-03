using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWindow : Appliance
{
    // What does an order window need??
    // Need to have some sort of relationship with the order manager

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    protected override bool WillAcceptItem(Item givenItem)
    {
        if(givenItem is Plate){
            Plate plate = (Plate) givenItem;
            if(plate.IsHoldingDish()){
                return true;
            }
        }
        return false;
    }

    protected override void AcceptItem(ItemSystem agentItems)
    {
        base.AcceptItem(agentItems);
    }

    protected override void HandleItem(Item givenItem)
    {
        throw new System.NotImplementedException();
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
}
