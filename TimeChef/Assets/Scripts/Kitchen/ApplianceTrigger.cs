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
        if(Input.GetKeyDown(KeyCode.E)){
            appliance.UseAppliance(agentItems);
        }
    }
}
