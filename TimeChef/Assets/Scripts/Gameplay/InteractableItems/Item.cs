using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum InteractionType {
        PickUp,
        Utilize
    }

    public InteractionType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if(col.tag == "Player"){
    //         col.gameObject.GetComponent<ItemSystem>()
    //     }
    // }

    public void Interact()
    {
        switch(type){
            case InteractionType.PickUp:
                Debug.Log("Picking up item");
                break;
            case InteractionType.Utilize:
                Debug.Log("Using item");
                break;
        }
    }
}
