using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractionTrigger : MonoBehaviour
{
    // private Appliance appliance;
    // private ItemSystem agentItems;
    protected bool inRange = false;
    protected GameObject gameplayAgent;
    protected ItemSystem agentItems;

    //public UnityEvent interactionEvent;
    //public KeyCode interactionKey;
    // void Awake()
    // {
    //     appliance = this.gameObject.transform.parent.GetComponent<Appliance>();
    // }
    protected abstract void Awake();
    protected virtual void Update()
    {
        if(inRange){
            Interact();
        }
    }
    protected abstract void Interact();
    
    // void Update()
    // {
    //     // if(inRange)
    //     // {
    //     //     if(Input.GetKeyDown(interactionKey)){
    //     //         //interactionEvent.Invoke();
    //     //         inRange = false;
    //     //     }
    //     //     //appliance.UseAppliance(agentItems);
    //     // }
    // }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Agent"){
            // Then appliance can be used
            Debug.Log("item can be taken or interacted with");
            inRange = true;
            gameplayAgent = col.gameObject;
            agentItems = gameplayAgent.GetComponent<ItemSystem>();
            //agentItems = col.gameObject.GetComponent<ItemSystem>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        inRange = false;
        gameplayAgent = null;
        agentItems = null;
    }

    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        inRange = false;
    }

    public void Activate()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
