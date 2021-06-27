using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Appliance
{
    // Stove needs to know when a pan or pot is removed
    private Kitchenware cookingTool;
    public Transform itemHolder;
    private bool isHolding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(cookingTool != null)
        {
            Debug.Log(cookingTool);
        }
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        if(givenItem is Kitchenware){
            return true;
        }
        return false;
    }

    protected override void HandleItem(Item givenItem)
    {
        cookingTool = (Kitchenware) givenItem;
        cookingTool.gameObject.transform.parent = itemHolder;
        cookingTool.gameObject.transform.position = itemHolder.position;
        cookingTool.ActivateInteraction();
        isHolding = true;
    }

    public void RemoveItem()
    {
        cookingTool = null;
    }

    protected override void Action()
    {
        // Accept a pan or pot (cookware)
        // adding ingredients to a cookware will start the process
        // 
        throw new System.NotImplementedException();
    }
}
