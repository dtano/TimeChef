using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    // private Appliance appliance;
    // private ItemSystem agentItems;
    private bool inRange = false;

    public UnityEvent interactionEvent;
    public KeyCode interactionKey;
    // void Awake()
    // {
    //     appliance = this.gameObject.transform.parent.GetComponent<Appliance>();
    // }
    
    void Update()
    {
        if(inRange)
        {
            if(Input.GetKeyDown(interactionKey)){
                interactionEvent.Invoke();
                inRange = false;
            }
            //appliance.UseAppliance(agentItems);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Agent"){
            // Then appliance can be used
            inRange = true;
            //agentItems = col.gameObject.GetComponent<ItemSystem>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        inRange = false;
        //agentItems = null;
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
