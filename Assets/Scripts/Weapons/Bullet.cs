using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }
    public void Deactivate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        rb.velocity = new Vector2(0f, bulletSpeed) * transform.up;
    }
    public void Activate()
    {
        gameObject.SetActive(true);
        rb.velocity = new Vector2(0f, bulletSpeed) * transform.up;
    }
    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            Deactivate();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Deactivate();
    }
}
