using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;


public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;


    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
      objectPool = new ObjectPool<Bullet>(CreateBullet,OnGetFromPool,OnReleaseToPool,OnDestroyPooledObject, collectionCheck,defaultCapacity,maxSize);
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
        if (Time.time > timer && objectPool != null && Player.Instance.hasWeapon == true)
        {
            Bullet bulletObject = objectPool.Get();
            if (bulletObject != null)
            {
                bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                timer = Time.time + shootIntervalInSeconds;
            }
        }
    }
}