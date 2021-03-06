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

    private SpriteRenderer spriteRenderer;
    private AudioManager audioManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // if(currItem == null){
        //     Debug.Log("No carry");
        //     // Do a raycast check 
        //     if(DetectItem()){
        //         Debug.Log("Detected an item");
        //         // Indicate that the item can be picked up
        //         if(Input.GetKeyDown(KeyCode.Space)){
        //             // Make the detected object a child of the player and place it
        //             // in the carryPoint. change currItem to point to this item
        //             Debug.Log("Initiating pick up sequence");
        //             PickUp();
        //         }
        //     }
        // }else{
        //     if(Input.GetKeyDown(KeyCode.Q)){
        //         currItem.gameObject.transform.parent = null;
        //         //currItem.GetComponent<Rigidbody2D>().isKinematic = false;
        //         currItem.GetComponent<Collider2D>().enabled = true;
        //         currItem = null;
        //     }


        // }

        if(currItem != null){
            if(Input.GetKeyDown(KeyCode.Q)){
                currItem.gameObject.transform.parent = null;
                //currItem.GetComponent<Rigidbody2D>().isKinematic = false;
                currItem.GetComponent<Collider2D>().enabled = true;
                currItem.ActivateInteraction();
                currItem.ResetSortOrder();
                currItem = null;
                
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
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if(obj == null){
            detectedItem = null;
            return false;
        }else{
            if(obj.tag == "Item"){
                detectedItem = obj.gameObject;
                return true;
            }
        }
        return false;

        // // Raycast method
        // RaycastHit2D itemCheck = Physics2D.Raycast(detectionPoint.position, detectionPoint.right, detectionRadius, detectionLayer);
        // Debug.DrawRay(detectionPoint.position, detectionPoint.right, Color.green);
        // if(itemCheck.collider != null && itemCheck.collider.tag == "Item"){
        //     Debug.Log("Detected item");
        //     detectedItem = itemCheck.collider.gameObject;
        //     return true;
        // }
        // return false;
    }

    

    void PickUp()
    {
        currItem = detectedItem.GetComponent<Item>();
        currItem.transform.parent = carryPoint;
        currItem.transform.position = carryPoint.position;
        currItem.GetComponent<Rigidbody2D>().isKinematic = true;
        currItem.GetComponent<Collider2D>().enabled = false;

        currItem.ChangeSortOrder(spriteRenderer.sortingOrder + 1);
        audioManager.PlaySound("PickUp");
        
        // Change animation to pick up and play pick up sound effect
        detectedItem = null;
    } 

    // Receive an item from a spawner (In this case its gonna be the fridge)
    public bool GetItem(GameObject item)
    {   
        // Only possible if there are no items being carried
        // Detect item fails sometimes
        if(currItem == null && DetectItem()){
            detectedItem = item;
            // Pick up the item 
            PickUp();
            return true;
        }
        return false;
    }

    public void ForcePickUp(GameObject item)
    {
        detectedItem = item;
        PickUp();
        item.GetComponent<Item>().DeactivateInteraction();
        audioManager.PlaySound("PickUp");
    }

    public void DropItem()
    {
        if(currItem != null){
            currItem.gameObject.GetComponent<Collider2D>().enabled = true;
            //SpriteRenderer sr = currItem.gameObject.GetComponent<SpriteRenderer>();
            currItem.ResetSortOrder();
            currItem = null;
        }
        // Change player animation back to normal hands
    }

    public void Dispose()
    {
        if(currItem != null){
            Destroy(currItem.gameObject);
            currItem = null;
            // Change player animation back to normal hands
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    // }

    public bool isCarrying()
    {
        return currItem != null;
    }

    public Item GetCurrItem()
    {
        return currItem;
    }

    
}
