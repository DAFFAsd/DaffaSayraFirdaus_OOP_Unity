using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;
    [SerializeField] private Camera mainCamera;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Vector2 screenBounds;
    private Rigidbody2D rb;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = (-2) * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2) * maxSpeed / (timeToStop * timeToStop);
        SpriteRenderer shipSpriteRenderer = transform.Find("Ship").GetComponent<SpriteRenderer>();
        objectWidth = shipSpriteRenderer.bounds.extents.x;
        objectHeight = shipSpriteRenderer.bounds.extents.y;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
         Debug.Log($"Screen Width: {Screen.width}, Screen Height: {Screen.height}");
        Debug.Log($"Object Width: {objectWidth * 2}, Object Height: {objectHeight * 2}");
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput);
        Vector2 currentVelocity = rb.velocity;
        Vector2 friction = GetFriction();
        currentVelocity = new Vector2(
            moveVelocity.x * moveDirection.x + friction.x * moveDirection.x,
            moveVelocity.y * moveDirection.y + friction.y * moveDirection.y
        );
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed.x, maxSpeed.x);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxSpeed.y, maxSpeed.y);
        rb.velocity = currentVelocity;
        //kita panggil movebound() yang sebelumnya tidak terpakai untuk batas movement terhadap
        //kamera player.
        MoveBound();
    }

    public Vector2 GetFriction()
    {
        if (moveDirection == Vector2.zero)
        {
            return stopFriction;
        }
        else
        {
            return moveFriction;
        }
    }

    public void MoveBound()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        //di sini posisi ship+engine harus dipastikan centered
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > Mathf.Max(stopClamp.x, stopClamp.y);
    }
}