using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    // Currently carried item (Can only carry one at a time)
    private Item currItem;
    private GameObject detectedItem;
    public Transform carryPoint;
    
    public Transform detectionPoint;
    public float detectionRadius = 0.2f;
    public LayerMask detectionLayer;
    
    // Update is called once per frame
    void Update()
    {
        if(currItem == null){
            // Do a raycast check 
            if(DetectItem()){
                Debug.Log("Detected an item");
                // Indicate that the item can be picked up
                if(Input.GetKeyDown(KeyCode.Space)){
                    // Make the detected object a child of the player and place it
                    // in the carryPoint. change currItem to point to this item
                    Debug.Log("Initiating pick up sequence");
                    PickUp();
                }
            }
        }
        //else{
        //     if(Input.GetKeyDown(KeyCode.Space)){
        //         DropItem();
        //     }
        // }
    }

    bool DetectItem()
    {
        // Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        // if(obj == null){
        //     detectedItem = null;
        //     return false;
        // }else{
        //     if(obj.tag == "Item"){
        //         Debug.Log("Detected item");
        //         detectedItem = obj.gameObject;
        //         return true;
        //     }
        // }

        // Raycast method
        RaycastHit2D itemCheck = Physics2D.Raycast(detectionPoint.position, detectionPoint.right, detectionRadius, detectionLayer);
        Debug.DrawRay(detectionPoint.position, detectionPoint.right, Color.green);
        if(itemCheck.collider != null && itemCheck.collider.tag == "Item"){
            Debug.Log("Detected item");
            detectedItem = itemCheck.collider.gameObject;
            return true;
        }
        return false;
    }

    void PickUp()
    {
        currItem = detectedItem.GetComponent<Item>();
        currItem.transform.parent = carryPoint;
        currItem.transform.position = carryPoint.position;
        currItem.GetComponent<Rigidbody2D>().isKinematic = true;
        detectedItem = null;
    }

    // Receive an item from a spawner (In this case its gonna be the fridge)
    public void GetItem(GameObject item)
    {   
        // Only possible if there are no items being carried
        if(currItem == null){
            detectedItem = item;
            // Pick up the item 
            PickUp();
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    // }

    // This function is called when the object 
    void UseItem()
    {
        // Item object will be deleted?
        currItem = null;
    }

    
}
