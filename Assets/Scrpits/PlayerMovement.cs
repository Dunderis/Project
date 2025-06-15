using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Animator animator;
    public Transform player;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    private float moveInput;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");
        
        if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1); // Facing right
        else if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1); // Facing left
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        

        
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        


    }
    
    
}