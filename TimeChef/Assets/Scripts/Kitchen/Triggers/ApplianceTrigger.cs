using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceTrigger : InteractionTrigger
{
    private Appliance appliance;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        appliance = GetComponentInParent<Appliance>();
    }

    protected override void Interact()
    {
        //Debug.Log("Calling interact");
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Interact!");
            appliance.UseAppliance(agentItems);
        }

        // if(appliance is Table){
        //     if(Input.GetKeyDown(KeyCode.Space)){
        //         ((Table) appliance).TransferToPlayer(agentItems);
        //     }
        // }
    }
}
