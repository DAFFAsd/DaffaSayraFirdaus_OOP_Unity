using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    private float horizontalSpeed = 3f;
    private float minX, maxX;
    private float direction;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;


    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }
    private void OnGetFromPool(Bullet objectPool)
    {
        objectPool.gameObject.SetActive(true);
    }
    private void OnReleaseToPool(Bullet objectPool)
    {
        objectPool.gameObject.SetActive(false);
    }
    private void OnDestroyPooledObject(Bullet objectPool)
    {
        Destroy(objectPool.gameObject);
    }
    private void FixedUpdate()
    {
        if (Time.time > timer && objectPool != null)
        {
            Bullet bulletObject = objectPool.Get();
            bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timer = Time.time + 1f;
        }
    }
    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + 1f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 1f;
        float minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1f;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1f;
        float spawnY = Random.Range(0, maxY);
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
