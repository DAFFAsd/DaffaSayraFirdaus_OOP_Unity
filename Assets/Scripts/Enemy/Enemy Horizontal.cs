using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float horizontalSpeed = 2f;
    private float minX, maxX;
    private float direction;

    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        float minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        float spawnY = Random.Range(minY, maxY);
        transform.position = new Vector3(
            Random.value < 0.5f ? minX : maxX,
            spawnY,
            transform.position.z
        );
        direction = transform.position.x < 0 ? 1f : -1f;
    }

    void Update()
    {
        transform.Translate(Vector3.right * horizontalSpeed * direction * Time.deltaTime);
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            // balik arah kalau dah mepet/nabrak boundary
            direction *= -1;
        }
    }
}
