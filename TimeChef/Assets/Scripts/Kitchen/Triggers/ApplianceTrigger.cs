using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceTrigger : InteractionTrigger
{
    private Appliance appliance;
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        appliance = GetComponentInParent<Appliance>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    protected override void Interact()
    {
        //Debug.Log("Calling interact");
        if(Input.GetKeyDown(KeyCode.E)){
            appliance.UseAppliance(agentItems);
        }

        // There's a sound effect bug
        if(Input.GetKeyDown(KeyCode.F)){
            if(appliance.AbleToManipulate() && !appliance.IsManipulated()){
                TimeManipulator manipulator = gameplayAgent.GetComponent<TimeManipulator>();
                appliance.ManipulateTime(manipulator);
                if(speedUpText != null){
                    speedUpText.enabled = false;
                }
                audioManager.PlaySound("FastForward");
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
