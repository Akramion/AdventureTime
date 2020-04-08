using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private bool isJump = false;

    private Animator animator;

    private bool m_FacingRight = true;
    private Grounded grounded;
    private Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        grounded = gameObject.transform.Find("Grounded").gameObject.GetComponent<Grounded>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump")) {
            Jump();
        }

        float movement = Input.GetAxis("Horizontal");
        Vector3 movementVec = new Vector3(movement, 0f, 0f);
        transform.position += movementVec * Time.deltaTime * moveSpeed;

        if (movement > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement < 0 && m_FacingRight)
        {
            Flip();
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

    private void Jump()
    {
        if(grounded.isGrounded == true) {
            rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
