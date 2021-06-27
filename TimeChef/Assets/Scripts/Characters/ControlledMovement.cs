using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMovement : MonoBehaviour
{
    public float moveSpeed;
    private float currSpeed = 0f;
    private Rigidbody2D rb;
    private Animator animator;

    public Transform detectionPoint;

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

        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        //detectionPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(movement.x != 0 || movement.y != 0){
            currSpeed = 1;
            detectionPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }else{
            currSpeed = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
