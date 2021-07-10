using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMovement : MonoBehaviour
{
    public float moveSpeed;
    private float currSpeed = 0f;
    private Rigidbody2D rb;
    private Animator animator;
    private ItemSystem itemSystem;

    public Transform detectionPoint;

    private Vector2 movement;

    private bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        itemSystem = GetComponent<ItemSystem>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            //detectionPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Animate();

            if(movement.x != 0 || movement.y != 0){
                currSpeed = 1;
                detectionPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }else{
                currSpeed = 0;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Animate()
    {
        // This check needed to fix bug where the player's direction resets when they stop moving
        animator.SetBool("IsCarrying", itemSystem.GetCurrItem() != null);
        if(movement != Vector2.zero){
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    public void Freeze()
    {
        canMove = false;
    }

    public void AllowMovement()
    {
        canMove = true;
    }
}
