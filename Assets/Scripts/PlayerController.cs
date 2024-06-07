using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float pushForce = 10f; // Besarnya gaya dorongan yang akan diberikan
    private Vector2 input;
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        PhysicsMaterial2D lowFrictionMaterial = new PhysicsMaterial2D();
        lowFrictionMaterial.friction = 0.1f; 
        lowFrictionMaterial.bounciness = 0.0f; 

        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.sharedMaterial = lowFrictionMaterial;

        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //Debug.Log("This is input.x: " + input.x);
        //Debug.Log("This is input.y: " + input.y);

        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            Vector2 targetPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }
    }

    
}