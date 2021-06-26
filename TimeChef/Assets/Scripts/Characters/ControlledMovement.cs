using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMovement : MonoBehaviour
{
    public float moveSpeed;
    private float currSpeed = 0f;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0 || movement.y != 0){
            currSpeed = 1;
        }else{
            currSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
