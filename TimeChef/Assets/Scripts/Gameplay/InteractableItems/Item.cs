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

    public abstract void Reset();
    protected int originalOrder;
    protected SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }
    void Awake()
    {
        triggerZone = GetComponentInChildren<InteractionTrigger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalOrder = spriteRenderer.sortingOrder;
    }

    public void ActivateInteraction()
    {
        triggerZone.Activate();
    }

    public void DeactivateInteraction()
    {
        triggerZone.Deactivate();
    }

    public void ChangeSortOrder(int newSort)
    {
        spriteRenderer.sortingOrder = newSort;
    }

    public void ResetSortOrder()
    {
        spriteRenderer.sortingOrder = originalOrder;
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    public void ShowSprite()
    {
        spriteRenderer.enabled = true;
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
