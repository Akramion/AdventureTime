﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 5f;
    public float jumpForce = 5f;

    private bool isJump = true;

    private Animator animator;

    private bool m_FacingRight = true;
    private Grounded grounded;
    private Rigidbody2D rigidbody;
    private float movement;

    private Camera camera;
    private Vector3 outsidePlayer;
    private SceneController sceneController;

    SoundManager soundManager;


    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        grounded = GameObject.Find("Grounded").gameObject.GetComponent<Grounded>();

        camera = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Update() {
        if(Input.GetButtonDown("Jump")) {
            Jump();
        }     

        Vector3 outsidePlayer = camera.WorldToViewportPoint(transform.position);

        // Смерть от падения вниз
        if(outsidePlayer.y < 0f) {

            // Звук смерти от падения
            soundManager.Play("fallingDeath");

            sceneController.LoadCurrentLevel();
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
            animator.SetBool("isGround", false);
        }

        else if(rigidbody.velocity.y < 0) {
            animator.SetBool("isFall", true);
            animator.SetBool("isJump", false);
        }

        else if(isJump == true && grounded.isGrounded == true) {
            animator.SetBool("isFall", false);
            animator.SetBool("isJump", false);
            animator.SetBool("isGround", true);
        }   
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(grounded.isGrounded == true) {
            Movement();
        }

        else{
            AirControl();
        }
    }

    private void AirControl() {
        soundManager.StopSound("movement");

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

        // Поворот спрайта
        if (movement > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movement < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Movement() {

        // Получаем нажатие кнопок лево и право
        movement = Input.GetAxis("Horizontal");

        // Создаем новый вектор скорости по горизонтали
        Vector2 movementVec = new Vector2(movement, 0f);

        // Получаем скорость перса
        Vector2 currentVelocity = rigidbody.velocity;

        // Присваиваем новую скорость
        currentVelocity.x = movementVec.x * moveSpeed;
        rigidbody.velocity = currentVelocity;
        
        
        // Звук передвижения
        if(movement != 0) {
            soundManager.PlayOneShot("movement");
        }

        else {
            soundManager.StopSound("movement");
        }


        // Поворот спрайта
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
        // Если перс на земле
        if(grounded.isGrounded == true) {
            // Выключения звука передвижения
            soundManager.StopSound("movement");
            soundManager.Play("jump");
            
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
