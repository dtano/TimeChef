using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : InteractionTrigger
{
    // Start is called before the first frame update
    private Item item;
    protected override void Awake()
    {
        item = GetComponentInParent<Item>();
    }

    protected override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(transform.parent.gameObject.name);
            if(agentItems.GetItem(transform.parent.gameObject)){
                Deactivate();
            }
        }
    }
}
