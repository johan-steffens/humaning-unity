using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    private float multiplierX = 0f;
    private float multiplierY = 0f;

    private Rigidbody2D slowRigidbody;

    void Start()
    {
        slowRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Make velocity always want to be 0
        Vector2 force = new Vector2(-slowRigidbody.velocity.x, -slowRigidbody.velocity.y);

        // Ceiling for x
        if(Mathf.Abs(slowRigidbody.velocity.x) >= 0.2f)
        {
            slowRigidbody.AddForce(new Vector2(force.x + multiplierX, 0));
            multiplierX += (force.x < 0 ? -1 : 1) * 0.1f;
        } else
        {
            slowRigidbody.velocity = new Vector2(0, slowRigidbody.velocity.y);
        }

        // Ceiling for y
        if (Mathf.Abs(slowRigidbody.velocity.y) >= 0.2f)
        {
            slowRigidbody.AddForce(new Vector2(0, force.y + multiplierY));
            multiplierY += (force.y < 0 ? -1 : 1) * 0.1f;
        }
        else
        {
            slowRigidbody.velocity = new Vector2(slowRigidbody.velocity.x, 0);
        }
    }
}
