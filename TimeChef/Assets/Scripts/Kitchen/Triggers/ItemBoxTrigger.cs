using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxTrigger : InteractionTrigger
{
    private IngredientBox ingBox;
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        ingBox = GetComponentInParent<IngredientBox>();
    }

    protected override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            ingBox.Produce(agentItems);
        }
    }

    protected override void TriggerEffect()
    {
        // Do nothing
    }

    protected override void ExitEffect()
    {
    }
}
