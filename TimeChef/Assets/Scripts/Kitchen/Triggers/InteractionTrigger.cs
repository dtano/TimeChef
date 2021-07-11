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
     // Text that appears when the player is in the trigger zone of a tool that can be sped up
    public TMPro.TextMeshProUGUI speedUpText;

    protected abstract void OnAwake();
    void Awake()
    {
        if(speedUpText != null){
            speedUpText.text = "Speed Up";
            speedUpText.enabled = false;
        }
        OnAwake();
    }
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
            inRange = true;
            gameplayAgent = col.gameObject;
            agentItems = gameplayAgent.GetComponent<ItemSystem>();
            TriggerEffect();
            //agentItems = col.gameObject.GetComponent<ItemSystem>();
        }
    }

    // void OnTriggerStay2D(Collider2D col)
    // {
    //     if(col.tag == "Agent"){
    //         // Then appliance can be used
    //         Debug.Log("item can be taken or interacted with");
    //         inRange = true;
    //         gameplayAgent = col.gameObject;
    //         agentItems = gameplayAgent.GetComponent<ItemSystem>();
    //         //agentItems = col.gameObject.GetComponent<ItemSystem>();
    //     }
    // }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Agent"){
            inRange = false;
            gameplayAgent = null;
            agentItems = null;
            ExitEffect();
        }
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

    protected virtual void TriggerEffect()
    {
        if(speedUpText != null && TriggerCondition()){
            Vector3 offset = new Vector3(0, 1f, 0);
            speedUpText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            speedUpText.enabled = true;
        }
    }
    
    protected virtual void ExitEffect()
    {
        if(speedUpText != null){
            speedUpText.enabled = false;
        }
    }

    protected abstract bool TriggerCondition();
}
