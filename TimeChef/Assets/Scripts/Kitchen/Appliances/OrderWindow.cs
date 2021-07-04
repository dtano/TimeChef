using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWindow : Appliance
{
    // What does an order window need??
    // Need to have some sort of relationship with the order manager
    public OrderManager orderManager;

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
        HandleItem(agentItems.GetCurrItem());
        if(agentItems.GetCurrItem() == null){
            agentItems.DropItem();
        }
    }

    protected override void HandleItem(Item givenItem)
    {
        if(orderManager.HasOrders()){
            orderManager.SubmitOrder((Plate) givenItem);
            givenItem.Reset();
            Destroy(givenItem.gameObject);
        }
        // Now we need to somehow pass this dish to the order manager
    }

    protected override void Action()
    {
    }
}
