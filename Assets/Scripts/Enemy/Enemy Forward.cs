using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float verticalSpeed = 2f;
    private float minY, maxY;

    void Start()
    {
        minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y - 1f;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1f;

        transform.position = new Vector3(
            Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f, Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f),
            maxY,
            transform.position.z
        );
    }
    void Update()
    {
        transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        transform.position = new Vector3(
            Random.Range(Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f, Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f),
            maxY,
            transform.position.z
        );
    }
}
