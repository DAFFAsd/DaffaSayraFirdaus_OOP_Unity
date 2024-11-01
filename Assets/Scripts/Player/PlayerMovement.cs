using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = new Vector2(
            2 * maxSpeed.x / timeToFullSpeed.x,
            2 * maxSpeed.y / timeToFullSpeed.y
        );
        
        moveFriction = new Vector2(
            -2 * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2 * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );
        
        stopFriction = new Vector2(
            -2 * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2 * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 currentVelocity = rb.velocity;
        Vector2 friction = GetFriction();
        if (horizontalInput != 0)
        {
            currentVelocity.x += moveDirection.x * moveVelocity.x * Time.fixedDeltaTime;
            currentVelocity.x += friction.x * Time.fixedDeltaTime;
        }
        else
        {
            currentVelocity.x += friction.x * Time.fixedDeltaTime;
            if (Mathf.Abs(currentVelocity.x) < stopClamp.x)
            {
                currentVelocity.x = 0;
            }
        }
        if (verticalInput != 0)
        {
            currentVelocity.y += moveDirection.y * moveVelocity.y * Time.fixedDeltaTime;
            currentVelocity.y += friction.y * Time.fixedDeltaTime;
        }
        else
        {
            currentVelocity.y += friction.y * Time.fixedDeltaTime;
            if (Mathf.Abs(currentVelocity.y) < stopClamp.y)
            {
                currentVelocity.y = 0;
            }
        }
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed.x, maxSpeed.x);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxSpeed.y, maxSpeed.y);
        rb.velocity = currentVelocity;
    }

    public Vector2 GetFriction()
    {
        Vector2 friction = Vector2.zero;
        if (moveDirection.x != 0)
        {
            friction.x = moveFriction.x * Mathf.Sign(rb.velocity.x);
        }
        else
        {
            friction.x = stopFriction.x * Mathf.Sign(rb.velocity.x);
        }
        if (moveDirection.y != 0)
        {
            friction.y = moveFriction.y * Mathf.Sign(rb.velocity.y);
        }
        else
        {
            friction.y = stopFriction.y * Mathf.Sign(rb.velocity.y);
        }

        return friction;
    }

    public void MoveBound()
    {

    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > Mathf.Max(stopClamp.x, stopClamp.y);
    }
}