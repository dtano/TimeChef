using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Appliance
{
    public Transform itemHolder;
    public bool allowMultiple = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        // Any item will do
        if(!allowMultiple){
            if(itemHolder.transform.childCount == 0){
                return true;
            }
        }else{
            return true;
        }
        return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        //givenItem.gameObject.GetComponent<Collider2D>().enabled = false;
        givenItem.transform.parent = itemHolder;
        givenItem.transform.position = itemHolder.position;
        givenItem.ActivateInteraction();
    }

    public void PassItem(Item givenItem)
    {
        HandleItem(givenItem);
    }

    // public void TransferToPlayer(ItemSystem agentItems)
    // {
    //     if(itemHolder.transform.childCount > 0){
    //         agentItems.ForcePickUp(transform.GetChild(0).gameObject);
    //     }
    // }

    public int GetNumItems()
    {
        return itemHolder.transform.childCount;
    }

    public GameObject GetTopObj()
    {
        return itemHolder.transform.GetChild(0).gameObject;
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
}
