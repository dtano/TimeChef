using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Appliance
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override bool WillAcceptItem(Item givenItem)
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleItem(Item givenItem)
    {
        throw new System.NotImplementedException();
    }

    protected override void Action()
    {
        throw new System.NotImplementedException();
    }
}
