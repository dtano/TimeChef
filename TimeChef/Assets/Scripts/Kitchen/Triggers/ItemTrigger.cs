using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : InteractionTrigger
{
    // Start is called before the first frame update
    private Item item;
    protected override void OnAwake()
    {
        item = GetComponentInParent<Item>();
    }

    protected override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(agentItems.GetItem(transform.parent.gameObject)){
                Deactivate();
            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            if(item.AbleToManipulate()){
                TimeManipulator manipulator = gameplayAgent.GetComponent<TimeManipulator>();
                item.ManipulateTime(manipulator);
            }
        }
    }

    protected override bool TriggerCondition()
    {
        return false;
    }
}
