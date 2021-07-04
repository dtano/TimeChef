using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Appliance
{
    public Transform itemHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        // Any item will do
        if(itemHolder.transform.childCount == 0){
            return true;
        }
        return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        givenItem.transform.parent = itemHolder;
        givenItem.transform.position = itemHolder.position;
        givenItem.ActivateInteraction();
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
}
