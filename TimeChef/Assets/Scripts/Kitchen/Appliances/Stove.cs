using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Appliance
{
    // Stove needs to know when a pan or pot is removed
    private Kitchenware cookingTool;
    public Transform itemHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timeEffect = new TimeAccelerator();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isHolding && cookingTool.transform.parent != itemHolder.transform){
            RemoveTool();
            // Turn off burner and stop timer
        }
        
        if(cookingTool != null)
        {
            if(cookingTool.IsCooking()){
                // Turn stove on 
            }
            Debug.Log(cookingTool);
        }

        Animate();
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        if(!isHolding){
            if(givenItem is Kitchenware){
                return true;
            }
        }
        // else{
        //     if(givenItem is Ingredient){
        //         return true;
        //     }
        // }
        return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        cookingTool = (Kitchenware) givenItem;
        cookingTool.PlaceOnAppliance(this);
        
        
        cookingTool.gameObject.transform.parent = itemHolder;
        cookingTool.gameObject.transform.position = itemHolder.position;
        cookingTool.ActivateInteraction();
        isHolding = true;
    }

    public void RemoveTool()
    {
        Debug.Log("Stove not holding anything anymore");
        cookingTool.RemoveFromAppliance();
        isHolding = false;
        cookingTool = null;
    }

    void Animate()
    {
        if(cookingTool != null){
            animator.SetBool("isProcessing", cookingTool.IsCooking());
        }else{
            animator.SetBool("isProcessing", false);
        }
    }

    protected override void Action()
    {
        // Accept a pan or pot (cookware)
        // adding ingredients to a cookware will start the process
        // 
        throw new System.NotImplementedException();
    }

    public override bool AbleToManipulate()
    {
        bool condition = timeEffect != null && cookingTool != null && cookingTool.IsCooking();
        if(condition){
            // Not a great workaround. Can be fixed next time
            ((TimeAccelerator)timeEffect).SetTimer(cookingTool.GetTimer());
        }
        return condition;
    }
}
