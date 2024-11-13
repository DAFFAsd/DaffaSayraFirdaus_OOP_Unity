using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform player;
    private float speed = 2f;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        float spawnX = Random.value < 0.5f ? minX : maxX;
        float spawnY = Random.Range(minY, maxY);
        transform.position = new Vector3(spawnX, spawnY, transform.position.z);
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
