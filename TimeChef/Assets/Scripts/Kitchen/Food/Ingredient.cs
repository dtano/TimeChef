using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public bool isFresh = true;
    private bool isProcessed = false;
    public float processTime;
    private float currTimer = 0f;
    // Needs a variable that 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initiates processing counter
    public void Unspoil()
    {
        if(!isFresh){
            // Start timer
            if(currTimer >= processTime){
                isFresh = true;
                // Change sprite
            }
        }
    }

    // Initates process counter (unspoil would probably be a process)
    public void Process()
    {

    }
}
