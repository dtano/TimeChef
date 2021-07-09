using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceTrigger : InteractionTrigger
{
    private Appliance appliance;
    
    // Start is called before the first frame update
    protected override void OnAwake()
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

        if(Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Attempt to speed up process");
            if(appliance.AbleToManipulate()){
                TimeManipulator manipulator = gameplayAgent.GetComponent<TimeManipulator>();
                appliance.ManipulateTime(manipulator);
                if(speedUpText != null){
                    speedUpText.enabled = false;
                }
            }
        }

        // if(appliance is Table){
        //     if(Input.GetKeyDown(KeyCode.Space)){
        //         ((Table) appliance).TransferToPlayer(agentItems);
        //     }
        // }
    }

    protected override void TriggerEffect()
    {
        if(speedUpText != null && TriggerCondition()){
            //Vector3 offset = new Vector3(0, 1f, 0);
            //speedUpText.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            speedUpText.enabled = true;
        }
    }

    protected override bool TriggerCondition()
    {
        bool hasEnoughPoints = gameplayAgent.GetComponent<TimeManipulator>().CanManipulate(appliance.timeCost);
        return hasEnoughPoints && appliance.isBusy();
    }
}
