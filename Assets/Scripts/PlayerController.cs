﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 5f;
    public float jumpForce = 5f;

    private bool isJump = false;

    private Animator animator;

    private bool m_FacingRight = true;
    private Grounded grounded;
    private Rigidbody2D rigidbody;
    private float movement;

    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        grounded = gameObject.transform.Find("Grounded").gameObject.GetComponent<Grounded>();
    }

    void Start()
    {
        
    }

    private void Update() {
        if(Input.GetButtonDown("Jump")) {
            Jump();
        }     

// Animations
        if(movement == 0) {
            animator.SetBool("isWalk", false);
        }

        else {
            animator.SetBool("isWalk", true);
        }

        if(isJump == true && rigidbody.velocity.y > 0) {
            animator.SetBool("isJump", true);
        }

        else if(rigidbody.velocity.y < 0) {
            animator.SetBool("isFall", true);
            animator.SetBool("isJump", false);
        }

        else if(isJump == true && rigidbody.velocity.y == 0) {
            animator.SetBool("isFall", false);
            animator.SetBool("isJump", false);
            animator.SetBool("isGround", true);
        }   
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        movement = Input.GetAxis("Horizontal");
        Vector2 movementVec = new Vector2(movement, 0f);
        rigidbody.AddForce(movementVec * acceleration);

        if(rigidbody.velocity.x > moveSpeed) {
            Vector2 vectorClamp = rigidbody.velocity;
            vectorClamp.x = moveSpeed;
            rigidbody.velocity = vectorClamp;
        }

        else if(rigidbody.velocity.x < -moveSpeed) {
            Vector2 vectorClamp = rigidbody.velocity;
            vectorClamp.x = -moveSpeed;
            rigidbody.velocity = vectorClamp;
        }


        if (movement > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement < 0 && m_FacingRight)
        {
            Flip();
        }


    }

    private void Jump()
    {
        if(grounded.isGrounded == true) {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJump = true;
        }
        
    }


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
