using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Appliance : MonoBehaviour
{
    protected Item interactedItem;
    public float processingTime;
    private bool isProcessing = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(isProcessing){
            //Start a timer here 
        }
    }

    // void OnCollisionStay2D(Collision2D col)
    // {
    //     if(col.collider.gameObject.tag == "Agent"){
    //         Debug.Log(col.collider.gameObject.tag);
    //         ItemSystem agentItems = col.collider.gameObject.GetComponent<ItemSystem>();
            
    //     }
    // }

    protected abstract void Action();
    protected abstract bool WillAcceptItem(Item givenItem);
    protected abstract void HandleItem(Item givenItem);

    public void UseAppliance(ItemSystem agentItems)
    {
        if(agentItems.isCarrying()){
                Debug.Log("Player can use this appliance");
                // Means that the player is able to use this appliance
                if(Input.GetKeyDown(KeyCode.E)){
                    if(WillAcceptItem(agentItems.GetCurrItem())){
                        // Take the item
                        Debug.Log("Item accepted");
                        AcceptItem(agentItems);
                    }else{
                        Debug.Log("Item declined");
                        DeclineItem();
                    }
                }
            }else{
                Debug.Log("Can't use this application");
            }
    }

    void AcceptItem(ItemSystem agentItems)
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
}
