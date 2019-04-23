using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    public int maxXVelocity;
    public int maxYVelocity;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Slow slow;

    private Vector2 moveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        slow = GetComponent<Slow>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get and normalise input
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Set rotation to follow velocity
        if(moveVelocity.x != 0 || moveVelocity.y != 0)
        {
            Vector3 vectorToTarget = (transform.position + new Vector3(moveVelocity.x, moveVelocity.y)) - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }

        // Slow if space is held
        if(Input.GetKey(KeyCode.Space))
        {
            slow.enabled = true;
        } else
        {
            slow.enabled = false;
        }
    }

    void FixedUpdate()
    {
        // Add x velocity
        if(moveVelocity.x != 0)
        {
            rigidbody.AddForce(new Vector2(moveVelocity.x, 0));
        }

        // Add y velocity
        if (moveVelocity.y != 0)
        {
            rigidbody.AddForce(new Vector2(0, moveVelocity.y));
        }

        // Cap x velocity
        if (moveVelocity.x < 0 && rigidbody.velocity.x < -maxXVelocity)
        {
            rigidbody.velocity = new Vector2(-maxXVelocity, rigidbody.velocity.y);
        }
        else if (moveVelocity.x > 0 && rigidbody.velocity.x > maxXVelocity)
        {
            rigidbody.velocity = new Vector2(maxXVelocity, rigidbody.velocity.y);
        }

        // Cap y velocity
        if (moveVelocity.y < 0 && rigidbody.velocity.y < -maxYVelocity)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -maxYVelocity);
        }
        else if (moveVelocity.y > 0 && rigidbody.velocity.y > maxYVelocity)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, maxYVelocity);
        }

        // Set animation speed
        animator.speed = 0.25f + (float) (Mathf.Abs(rigidbody.velocity.x) + Mathf.Abs(rigidbody.velocity.y)) / (float) (maxXVelocity + maxYVelocity);
    }
   
}
