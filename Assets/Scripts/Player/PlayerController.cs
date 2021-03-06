﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public int maxXVelocity;
    public int maxYVelocity;

    private Animator animator;
    private Rigidbody2D playerRigidbody;
    private Slow slow;

    private Vector2 moveVelocity;

    private Vector2 originalVelocity = Vector2.zero;
    public float brakeTime = 1.5f;
    private float brakeTimer = 0;
    private bool braking = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slow = GetComponent<Slow>();
    }

    // Update is called once per frame
    void Update()
    {
        // Slow if space is held or user not pressing movement keys
        if (Input.GetKey(KeyCode.Space) || (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0))
        {
            if (! braking)
            {
                brakeTimer = 0;
                originalVelocity = playerRigidbody.velocity;
                braking = true;
            }

            playerRigidbody.velocity = Vector2.Lerp(originalVelocity, Vector2.zero, brakeTimer);
            brakeTimer += Time.deltaTime / brakeTime;
        }
        else
        {
            braking = false;

            if (MainSceneController.GetInstance().GetState() == Game.State.CATCHING)
            {
                // Get and normalise input
                Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                moveVelocity = moveInput.normalized * speed;

                // Set rotation to follow velocity
                if (moveVelocity.x != 0 || moveVelocity.y != 0)
                {
                    Vector3 vectorToTarget = (transform.position + new Vector3(moveVelocity.x, moveVelocity.y)) - transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(! braking)
        {
            // Add x velocity
            if (moveVelocity.x != 0)
            {
                playerRigidbody.AddForce(new Vector2(moveVelocity.x, 0));
            }

            // Add y velocity
            if (moveVelocity.y != 0)
            {
                playerRigidbody.AddForce(new Vector2(0, moveVelocity.y));
            }

            // Cap x velocity
            if (moveVelocity.x < 0 && playerRigidbody.velocity.x < -maxXVelocity)
            {
                playerRigidbody.velocity = new Vector2(-maxXVelocity, playerRigidbody.velocity.y);
            }
            else if (moveVelocity.x > 0 && playerRigidbody.velocity.x > maxXVelocity)
            {
                playerRigidbody.velocity = new Vector2(maxXVelocity, playerRigidbody.velocity.y);
            }

            // Cap y velocity
            if (moveVelocity.y < 0 && playerRigidbody.velocity.y < -maxYVelocity)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -maxYVelocity);
            }
            else if (moveVelocity.y > 0 && playerRigidbody.velocity.y > maxYVelocity)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, maxYVelocity);
            }
        }        

        // Set animation speed
        animator.speed = 0.25f + (float) (Mathf.Abs(playerRigidbody.velocity.x) + Mathf.Abs(playerRigidbody.velocity.y)) / (float) (maxXVelocity + maxYVelocity);
    }
   
}
