using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum InteractionType {
        PickUp,
        Tool
    }

    public InteractionType type;
    private InteractionTrigger triggerZone;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }
    void Awake()
    {
        triggerZone = GetComponentInChildren<InteractionTrigger>();
    }

    public void ActivateInteraction()
    {
        triggerZone.Activate();
    }

    

    // public void Interact()
    // {
    //     switch(type){
    //         case InteractionType.PickUp:
    //             Debug.Log("Picking up item");
    //             break;
    //         case InteractionType.Utilize:
    //             Debug.Log("Using item");
    //             break;
    //     }
    // }
}
